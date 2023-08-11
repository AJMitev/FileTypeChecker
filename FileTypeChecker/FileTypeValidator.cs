namespace FileTypeChecker
{
    using FileTypeChecker.Abstracts;
    using FileTypeChecker.Common;
    using FileTypeChecker.Exceptions;
    using FileTypeChecker.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    /// <summary>
    /// Provides you a method for file type validation.
    /// </summary>
    public abstract class FileTypeValidator
    {
        private static readonly List<Assembly> typesAssemblies = new()
        {
            typeof(FileType).Assembly
        };

        private static bool isInitialized = false;
        private static readonly object initializationLock = new();
        private static readonly object whereLock = new();

        private static readonly HashSet<Type> knownTypes = new();
        private static readonly List<IFileType> fileTypes = new();

        protected static IReadOnlyCollection<IFileType> FileTypes
        {
            get
            {
                if (!isInitialized)
                {
                    lock (initializationLock)
                    {
                        if (!isInitialized)
                        {
                            RegisterTypes(typesAssemblies);
                            isInitialized = true;
                        }
                    }
                }

                return fileTypes;
            }
        }

        /// <summary>
        /// Checks that the particular type is supported.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>If current type is supported</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        public static bool IsTypeRecognizable(Stream fileContent)
        {
            DataValidator.ThrowIfNull(fileContent, nameof(Stream));

            if(fileContent.Length == 0)
                throw new ArgumentNullException(nameof(Stream));

            return FileTypes.Any(type => type.DoesMatchWith(fileContent));
        }

        /// <summary>
        /// Checks that the particular type is supported.
        /// </summary>
        /// <param name="byteContent">Content to be checked</param>
        /// <returns>If current type is supported</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        public static bool IsTypeRecognizable(byte[] byteContent)
        {
            DataValidator.ThrowIfNull(byteContent, nameof(Array));
            return FileTypes.Any(type => type.DoesMatchWith(byteContent));
        }

        /// <summary>
        /// Get details about current file type.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>Instance of <see cref="IFileType}"/> type. If the type is not recognized returns <see cref="null}"/></returns>
        /// /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static IFileType GetFileType(Stream fileContent)
        {
            DataValidator.ThrowIfNull(fileContent, nameof(Stream));

            return FindBestMatch(fileContent);
        }

        /// <summary>
        /// Gives you opportunity to register your custom types.
        /// </summary>
        /// <param name="assemblies">Assemblies that contains your custom types.</param>
        public static void RegisterCustomTypes(params Assembly[] assemblies)
        {
            DataValidator.ThrowIfNull(assemblies, nameof(Assembly));

            typesAssemblies.AddRange(assemblies);
            RegisterTypes(assemblies);
        }

        /// <summary>
        /// Validates that the file is from certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="fileContent">File as stream</param>
        /// <returns>True if file match the desired type otherwise returns false.</returns>
        public static bool Is<T>(Stream fileContent) where T : FileType, IFileType, new()
            => fileContent.Is<T>();

        /// <summary>
        /// Validates that the current file is image.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>Returns true if the provided file is image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF and PNG.</returns>
        public static bool IsImage(Stream fileContent)
                    => fileContent.IsImage();

        /// <summary>
        /// Validates that the current file is archive.
        /// </summary>
        /// <param name="fileContent"File to check as stream.></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar and Zip.</returns>
        public static bool IsArchive(Stream fileContent)
            => fileContent.IsArchive();

        internal static IFileType FindBestMatch(Stream fileContent)
        {
            var matches = FileTypes.Where(fileType => fileType.DoesMatchWith(fileContent));

            if (!matches.Any())
            {
                throw new TypeNotFoundException();
            }

            return ReturnBestMatch(fileContent, matches);
        }

        internal static IFileType FindBestMatch(byte[] content)
        {
            var matches = FileTypes.Where(fileType => fileType.DoesMatchWith(content));

            if (!matches.Any())
            {
                throw new TypeNotFoundException();
            }

            return ReturnBestMatch(content, matches);
        }

        private static IEnumerable<Type> GetTypesInstance(Assembly assembly)
            => assembly.GetTypes()
                    .Where(type => typeof(IFileType).IsAssignableFrom(type)
                                 && !type.IsAbstract
                                 && !type.IsInterface);

        private static void RegisterTypes(IEnumerable<Assembly> assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (var type in GetTypesInstance(assembly))
                {
                    if (knownTypes.Add(type))
                    {
                        var fileType = (IFileType)Activator.CreateInstance(type);
                        fileTypes.Add(fileType);
                    }
                }
            }
        }

        private static IFileType FindBestMatch(Stream fileContent, IEnumerable<IFileType> result)
        {
            try
            {
                var scoreboard = CreateScoreboard(fileContent, result);
                return FindMaxScore(scoreboard);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private static IFileType FindBestMatch(byte[] content, IEnumerable<IFileType> result)
        {
            try
            {
                var scoreboard = CreateScoreboard(content, result);
                return FindMaxScore(scoreboard);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private static IEnumerable<MatchScore> CreateScoreboard(Stream fileContent, IEnumerable<IFileType> result)
        {
            var scoreboard = new List<MatchScore>();

            for (int typeIndex = 0; typeIndex < result.Count(); typeIndex++)
            {
                var currentType = result.ElementAt(typeIndex) as FileType;
                var currentScore = currentType.GetMatchingNumber(fileContent);

                scoreboard.Add(new MatchScore(currentType, currentScore));
            }

            return scoreboard;
        }

        private static IEnumerable<MatchScore> CreateScoreboard(byte[] content, IEnumerable<IFileType> result)
        {
            var scoreboard = new List<MatchScore>();

            for (int typeIndex = 0; typeIndex < result.Count(); typeIndex++)
            {
                var currentType = result.ElementAt(typeIndex) as FileType;
                var currentScore = currentType.GetMatchingNumber(content);

                scoreboard.Add(new MatchScore(currentType, currentScore));
            }

            return scoreboard;
        }

        private static IFileType FindMaxScore(IEnumerable<MatchScore> matches)
        {
            if (matches.Count() == 0)
                throw new EmptyCollectionException();

            int maxScore = matches.Max(x => x.Score);
            var bestMatch = matches.Where(x => x.Score.Equals(maxScore));

            if (bestMatch.Count() > 1)
                throw new MoreThanOneTypeMatchesException(string.Join(", ", bestMatch.Select(x => x.Type.Extension)));

            return bestMatch.First().Type;
        }

        private static IFileType ReturnBestMatch(Stream fileContent, IEnumerable<IFileType> matches)
            => matches.Count() == 0
                ? null
                : matches.Count() == 1
                    ? matches.First()
                    : FindBestMatch(fileContent, matches);

        private static IFileType ReturnBestMatch(byte[] content, IEnumerable<IFileType> matches)
           => matches.Count() == 0
               ? null
               : matches.Count() == 1
                   ? matches.First()
                   : FindBestMatch(content, matches);
    }
}