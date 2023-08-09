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

            if (TryFindBestMatch(fileContent, out var fileType))
            {
                return fileType;
            }

            throw new TypeNotFoundException();
        }
        
        public static bool TryGetFileType(Stream fileContent, out IFileType fileType)
        {
            DataValidator.ThrowIfNull(fileContent, nameof(Stream));

            return TryFindBestMatch(fileContent, out fileType);
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

        internal static bool TryFindBestMatch(Stream fileContent, out IFileType fileType)
        {
            var matches = FileTypes.Where(fileType => fileType.DoesMatchWith(fileContent)).ToList();

            if (!matches.Any())
            {
                fileType = null;
                return false;
            }

            return TryReturnBestMatch(fileContent, matches, out fileType);
        }

        internal static bool TryFindBestMatch(byte[] content, out IFileType fileType)
        {
            var matches = FileTypes.Where(fileType => fileType.DoesMatchWith(content)).ToList();

            if (!matches.Any())
            {
                fileType = null;
                return false;
            }

            return TryReturnBestMatch(content, matches, out fileType);
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

        private static bool TryFindBestMatch(Stream fileContent, ICollection<IFileType> result, out IFileType fileType)
        {
            var scoreboard = CreateScoreboard(fileContent, result);
            return TryFindMaxScore(scoreboard, out fileType);
        }

        private static bool TryFindBestMatch(byte[] content, ICollection<IFileType> result, out IFileType fileType)
        {
            var scoreboard = CreateScoreboard(content, result);
            return TryFindMaxScore(scoreboard, out fileType);
        }

        private static ICollection<MatchScore> CreateScoreboard(Stream fileContent, ICollection<IFileType> result)
        {
            var scoreboard = new List<MatchScore>();

            for (int typeIndex = 0; typeIndex < result.Count(); typeIndex++)
            {
                var currentType = result.ElementAt(typeIndex);
                var currentScore = currentType.GetMatchingNumber(fileContent);

                scoreboard.Add(new MatchScore(currentType, currentScore));
            }

            return scoreboard;
        }

        private static ICollection<MatchScore> CreateScoreboard(byte[] content, ICollection<IFileType> result)
        {
            var scoreboard = new List<MatchScore>();

            for (int typeIndex = 0; typeIndex < result.Count(); typeIndex++)
            {
                var currentType = (FileType)result.ElementAt(typeIndex);
                var currentScore = currentType.GetMatchingNumber(content);

                scoreboard.Add(new MatchScore(currentType, currentScore));
            }

            return scoreboard;
        }
        
        private static bool TryFindMaxScore(ICollection<MatchScore> matches, out IFileType fileType)
        {
            if (!matches.Any())
            {
                fileType = null;
                return false;
            }

            int maxScore = matches.Max(x => x.Score);
            var bestMatch = matches.Where(x => x.Score.Equals(maxScore)).ToList();

            if (bestMatch.Count > 1)
            {
                fileType = null;
                return false;
            }

            fileType = bestMatch.First().Type;
            return true;
        }

        private static bool TryReturnBestMatch(Stream fileContent, ICollection<IFileType> matches, out IFileType fileType)
        {
            if (matches.Count == 0)
            {
                fileType = null;
                return false;
            }
            if (matches.Count == 1)
            {
                fileType = matches.First();
                return true;
            }
            return TryFindBestMatch(fileContent, matches, out fileType);
        }

        private static bool TryReturnBestMatch(byte[] fileContent, ICollection<IFileType> matches, out IFileType fileType)
        {
            if (matches.Count == 0)
            {
                fileType = null;
                return false;
            }
            if (matches.Count == 1)
            {
                fileType = matches.First();
                return true;
            }
            return TryFindBestMatch(fileContent, matches, out fileType);
        }
    }
}