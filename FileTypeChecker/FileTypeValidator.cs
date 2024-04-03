namespace FileTypeChecker
{
    using Abstracts;
    using Common;
    using Exceptions;
    using Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides you a method for file type validation.
    /// </summary>
    public abstract class FileTypeValidator
    {
        private static readonly List<Assembly> TypesAssemblies = new()
        {
            typeof(FileType).Assembly
        };

        private static bool _isInitialized;
        private static readonly object InitializationLock = new();

        private static readonly HashSet<Type> KnownTypes = new();
        private static readonly List<IFileType> FileTypes = new();

        /// <summary>
        /// The minimal buffer size required to validate all the known <see cref="FileType"/>s.
        /// </summary>
        internal static int MinimalBufferSize { private set; get; }

        private static IReadOnlyCollection<IFileType> Types
        {
            get
            {
                if (_isInitialized)
                    return FileTypes;

                lock (InitializationLock)
                {
                    if (_isInitialized)
                        return FileTypes;

                    RegisterTypes(TypesAssemblies);
                    _isInitialized = true;
                }

                return FileTypes;
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

            if (fileContent.Length == 0)
                throw new ArgumentNullException(nameof(Stream));

            return Types.Any(type => type.DoesMatchWith(fileContent));
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
            return Types.Any(type => type.DoesMatchWith(byteContent));
        }

        /// <summary>
        /// Get details about the type.
        /// </summary>
        /// <param name="fileContent">File content to check as stream.</param>
        /// <returns>Instance of <see cref="IFileType"/> type. If the type is not recognized returns null.</returns>
        /// <exception cref="System.ArgumentException"></exception>
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
        /// Get details about the type.
        /// </summary>
        /// <param name="byteContent">File content as a byte array.</param>
        /// <returns>Instance of <see cref="IFileType"/> type. If the type is not recognized returns null/></returns>
        /// /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static IFileType GetFileType(byte[] byteContent)
        {
            DataValidator.ThrowIfNull(byteContent, nameof(byteContent));

            return FindBestMatch(byteContent);
        }

        /// <summary>
        /// Try get
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns>Instance of <see cref="MatchResult"/> containing information about the success of searching and the type if found.</returns>
        public static MatchResult TryGetFileType(Stream fileContent)
        {
            try
            {
                DataValidator.ThrowIfNull(fileContent, nameof(Stream));
                var match = FindBestMatch(fileContent);
                return new MatchResult(match);
            }
            catch (Exception)
            {
                return new MatchResult(null);
            }
        }

        /// <summary>
        /// Gives you opportunity to register your custom types.
        /// </summary>
        /// <param name="assemblies">Assemblies that contains your custom types.</param>
        public static void RegisterCustomTypes(params Assembly[] assemblies)
        {
            DataValidator.ThrowIfNull(assemblies, nameof(Assembly));

            TypesAssemblies.AddRange(assemblies);
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
        /// <param name="fileContent">File to check as stream."></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar and Zip.</returns>
        public static bool IsArchive(Stream fileContent)
            => fileContent.IsArchive();

        /// <summary>
        /// Validates that the file is from certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="fileContent">File bytes</param>
        /// <returns>True if file match the desired type otherwise returns false.</returns>
        public static bool Is<T>(byte[] fileContent) where T : FileType, IFileType, new()
            => fileContent.Is<T>();

        /// <summary>
        /// Validates that the current file is image.
        /// </summary>
        /// <param name="fileContent">File bytes.</param>
        /// <returns>Returns true if the provided file is image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF and PNG.</returns>
        public static bool IsImage(byte[] fileContent)
            => fileContent.IsImage();

        /// <summary>
        /// Validates that the current file is archive.
        /// </summary>
        /// <param name="fileContent">File bytes."></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar and Zip.</returns>
        public static bool IsArchive(byte[] fileContent)
            => fileContent.IsArchive();
        internal static IFileType FindBestMatch(Stream fileContent)
        {
            var matches = Types
                .Where(fileType => fileType.DoesMatchWith(fileContent))
                .ToList();

            if (!matches.Any())
            {
                throw new TypeNotFoundException();
            }

            return ReturnBestMatch(fileContent, matches);
        }

        internal static IFileType FindBestMatch(byte[] content)
        {
            var matches = Types
                .Where(fileType => fileType.DoesMatchWith(content))
                .ToList();

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
                    if (KnownTypes.Add(type))
                    {
                        var fileType = (IFileType)Activator.CreateInstance(type);
                        FileTypes.Add(fileType);
                    }
                }
            }
            MinimalBufferSize = FileTypes.Max(o => o.MaxMagicSequenceLength);
        }

        private static IFileType FindBestMatch(Stream fileContent, IList<IFileType> result)
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

        private static IFileType FindBestMatch(byte[] content, IList<IFileType> result)
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

        private static IList<MatchScore> CreateScoreboard(Stream fileContent, IList<IFileType> result)
        {
            var scoreboard = new List<MatchScore>();

            for (var typeIndex = 0; typeIndex < result.Count(); typeIndex++)
            {
                if (result.ElementAt(typeIndex) is not FileType currentType)
                    throw new TypeNotFoundException();
                    
                var currentScore = currentType.GetMatchingNumber(fileContent);

                scoreboard.Add(new MatchScore(currentType, currentScore));
            }

            return scoreboard;
        }

        private static IList<MatchScore> CreateScoreboard(byte[] content, IList<IFileType> result)
        {
            var scoreboard = new List<MatchScore>();

            for (var typeIndex = 0; typeIndex < result.Count(); typeIndex++)
            {
                if (result.ElementAt(typeIndex) is not FileType currentType)
                    throw new TypeNotFoundException();
                
                var currentScore = currentType.GetMatchingNumber(content);

                scoreboard.Add(new MatchScore(currentType, currentScore));
            }

            return scoreboard;
        }

        private static IFileType FindMaxScore(IList<MatchScore> matches)
        {
            if (!matches.Any())
                throw new EmptyCollectionException();

            var maxScore = matches.Max(x => x.Score);
            var bestMatch = matches
                .Where(x => x.Score.Equals(maxScore))
                .ToList();

            if (bestMatch.Count() > 1)
                throw new MoreThanOneTypeMatchesException(string.Join(", ", bestMatch.Select(x => x.Type.Extension)));

            return bestMatch.First().Type;
        }

        private static IFileType ReturnBestMatch(Stream fileContent, IList<IFileType> matches)
            => matches.Any()
                ? matches.Count() == 1
                    ? matches.First()
                    : FindBestMatch(fileContent, matches)
                : null;

        private static IFileType ReturnBestMatch(byte[] content, IList<IFileType> matches)
            => matches.Any()
                ? matches.Count() == 1
                    ? matches.First()
                    : FindBestMatch(content, matches)
                : null;
    }
}