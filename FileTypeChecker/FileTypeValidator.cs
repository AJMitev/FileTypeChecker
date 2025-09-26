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
    using System.Threading;
    using System.Threading.Tasks;

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
        /// <param name="fileContent">File to check as a stream.</param>
        /// <returns>If the current type is supported</returns>
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
        /// <returns>If the current type is supported</returns>
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
        /// <returns>Instance of <see cref="IFileType"/> type. If the type is not recognized, it returns null.</returns>
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
        /// <returns>Instance of <see cref="IFileType"/> type. If the type is not recognized, it returns null/></returns>
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
        /// Try to get
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
        /// Gives you an opportunity to register your custom types.
        /// </summary>
        /// <param name="assemblies">Assemblies that contain your custom types.</param>
        public static void RegisterCustomTypes(params Assembly[] assemblies)
        {
            DataValidator.ThrowIfNull(assemblies, nameof(Assembly));

            TypesAssemblies.AddRange(assemblies);
            RegisterTypes(assemblies);
        }

        /// <summary>
        /// Validates that the file is from a certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="fileContent">File as a stream</param>
        /// <returns>True, if a file matches the desired type otherwise returns false.</returns>
        public static bool Is<T>(Stream fileContent) where T : FileType, IFileType, new()
            => fileContent.Is<T>();

        /// <summary>
        /// Validates that the current file is an image.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <returns>Returns true if the provided file is an image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF. and PNG.</returns>
        public static bool IsImage(Stream fileContent)
            => fileContent.IsImage();

        /// <summary>
        /// Validates that the current file is an archive.
        /// </summary>
        /// <param name="fileContent">File to check as stream."></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar and Zip.</returns>
        public static bool IsArchive(Stream fileContent)
            => fileContent.IsArchive();

        /// <summary>
        /// Validates that the file is from a certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="fileContent">File bytes</param>
        /// <returns>True, if a file matches the desired type otherwise returns false.</returns>
        public static bool Is<T>(byte[] fileContent) where T : FileType, IFileType, new()
            => fileContent.Is<T>();

        /// <summary>
        /// Validates that the current file is an image.
        /// </summary>
        /// <param name="fileContent">File bytes.</param>
        /// <returns>Returns true if the provided file is an image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF and PNG.</returns>
        public static bool IsImage(byte[] fileContent)
            => fileContent.IsImage();

        /// <summary>
        /// Validates that the current file is an archive.
        /// </summary>
        /// <param name="fileContent">File bytes."></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar and Zip.</returns>
        public static bool IsArchive(byte[] fileContent)
            => fileContent.IsArchive();

        // Async Methods

        /// <summary>
        /// Asynchronously checks that the particular type is supported.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the current type is supported; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="fileContent"/> is null or empty.</exception>
        /// <exception cref="System.NotSupportedException">Thrown when the stream does not support reading or seeking.</exception>
        /// <exception cref="System.ObjectDisposedException">Thrown when the stream has been disposed.</exception>
        /// <exception cref="System.OperationCanceledException">Thrown when the operation is canceled via the cancellation token.</exception>
        public static async Task<bool> IsTypeRecognizableAsync(Stream fileContent, CancellationToken cancellationToken = default)
        {
            DataValidator.ThrowIfNull(fileContent, nameof(Stream));

            if (fileContent.Length == 0)
                throw new ArgumentNullException(nameof(Stream));

            foreach (var type in Types)
            {
                if (await type.DoesMatchWithAsync(fileContent, resetPosition: true).ConfigureAwait(false))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Asynchronously gets details about the file type.
        /// </summary>
        /// <param name="fileContent">File content to check as stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an instance of <see cref="IFileType"/> if the type is recognized; otherwise, throws an exception.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="fileContent"/> is null.</exception>
        /// <exception cref="System.NotSupportedException">Thrown when the stream does not support reading or seeking.</exception>
        /// <exception cref="System.ObjectDisposedException">Thrown when the stream has been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when an internal error occurs during type matching.</exception>
        /// <exception cref="TypeNotFoundException">Thrown when the file type cannot be determined.</exception>
        /// <exception cref="System.OperationCanceledException">Thrown when the operation is canceled via the cancellation token.</exception>
        public static async Task<IFileType> GetFileTypeAsync(Stream fileContent, CancellationToken cancellationToken = default)
        {
            DataValidator.ThrowIfNull(fileContent, nameof(Stream));

            return await FindBestMatchAsync(fileContent, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously attempts to get the file type without throwing exceptions.
        /// </summary>
        /// <param name="fileContent">File content to check as stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="MatchResult"/> with information about the success of the search and the type if found.</returns>
        public static async Task<MatchResult> TryGetFileTypeAsync(Stream fileContent, CancellationToken cancellationToken = default)
        {
            try
            {
                DataValidator.ThrowIfNull(fileContent, nameof(Stream));
                var match = await FindBestMatchAsync(fileContent, cancellationToken).ConfigureAwait(false);
                return new MatchResult(match);
            }
            catch (Exception)
            {
                return new MatchResult(null);
            }
        }

        /// <summary>
        /// Asynchronously validates that the file is of a specific type.
        /// </summary>
        /// <typeparam name="T">The file type that implements <see cref="FileType"/> and <see cref="IFileType"/>.</typeparam>
        /// <param name="fileContent">File content as a stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the file matches the desired type; otherwise, false.</returns>
        public static async Task<bool> IsAsync<T>(Stream fileContent, CancellationToken cancellationToken = default) where T : FileType, IFileType, new()
            => await fileContent.IsAsync<T>(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously validates that the current file is an image.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the provided file is an image; otherwise, false. Supported image types include: Bitmap, JPEG, GIF, PNG, TIFF, WebP, PSD, HEIR, and ICO.</returns>
        public static async Task<bool> IsImageAsync(Stream fileContent, CancellationToken cancellationToken = default)
            => await fileContent.IsImageAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously validates that the current file is an archive.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the provided file is an archive; otherwise, false. Supported archive types include: ZIP, RAR, 7-Zip, TAR, GZIP, BZIP2, ZIP, and XZ.</returns>
        public static async Task<bool> IsArchiveAsync(Stream fileContent, CancellationToken cancellationToken = default)
            => await fileContent.IsArchiveAsync(cancellationToken).ConfigureAwait(false);

        // High-performance ReadOnlySpan<byte> overloads

        /// <summary>
        /// Checks that the particular type is supported using high-performance ReadOnlySpan.
        /// This overload avoids memory allocations and provides optimal performance.
        /// </summary>
        /// <param name="fileContent">File content as ReadOnlySpan of bytes.</param>
        /// <returns>True if the current type is supported; otherwise, false.</returns>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="fileContent"/> is empty.</exception>
        public static bool IsTypeRecognizable(System.ReadOnlySpan<byte> fileContent)
        {
            if (fileContent.IsEmpty)
                throw new ArgumentException("File content cannot be empty.", nameof(fileContent));

            foreach (var type in Types)
            {
                if (type.DoesMatchWith(fileContent))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Gets details about the file type using high-performance ReadOnlySpan.
        /// This overload avoids memory allocations and provides optimal performance.
        /// </summary>
        /// <param name="fileContent">File content as ReadOnlySpan of bytes.</param>
        /// <returns>Instance of <see cref="IFileType"/> if the type is recognized; otherwise, throws an exception.</returns>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="fileContent"/> is empty.</exception>
        /// <exception cref="TypeNotFoundException">Thrown when the file type cannot be determined.</exception>
        public static IFileType GetFileType(System.ReadOnlySpan<byte> fileContent)
        {
            if (fileContent.IsEmpty)
                throw new ArgumentException("File content cannot be empty.", nameof(fileContent));

            return FindBestMatch(fileContent);
        }

        /// <summary>
        /// Attempts to get the file type using high-performance ReadOnlySpan without throwing exceptions.
        /// This overload avoids memory allocations and provides optimal performance.
        /// </summary>
        /// <param name="fileContent">File content as ReadOnlySpan of bytes.</param>
        /// <returns>Instance of <see cref="MatchResult"/> with information about the success of the search and the type if found.</returns>
        public static MatchResult TryGetFileType(System.ReadOnlySpan<byte> fileContent)
        {
            try
            {
                if (fileContent.IsEmpty)
                    return new MatchResult(null);
                    
                var match = FindBestMatch(fileContent);
                return new MatchResult(match);
            }
            catch (Exception)
            {
                return new MatchResult(null);
            }
        }

        /// <summary>
        /// Validates that the file is of a specific type using high-performance ReadOnlySpan.
        /// This overload avoids memory allocations and provides optimal performance.
        /// </summary>
        /// <typeparam name="T">The file type that implements <see cref="FileType"/> and <see cref="IFileType"/>.</typeparam>
        /// <param name="fileContent">File content as ReadOnlySpan of bytes.</param>
        /// <returns>True if the file matches the desired type; otherwise, false.</returns>
        public static bool Is<T>(System.ReadOnlySpan<byte> fileContent) where T : FileType, IFileType, new()
            => fileContent.Is<T>();

        /// <summary>
        /// Validates that the current file is an image using high-performance ReadOnlySpan.
        /// This overload avoids memory allocations and provides optimal performance.
        /// </summary>
        /// <param name="fileContent">File content as ReadOnlySpan of bytes.</param>
        /// <returns>True if the provided file is an image; otherwise, false. Supported image types include: Bitmap, JPEG, GIF, PNG, TIFF, WebP, PSD, HEIC, and ICO.</returns>
        public static bool IsImage(System.ReadOnlySpan<byte> fileContent)
            => fileContent.IsImage();

        /// <summary>
        /// Validates that the current file is an archive using high-performance ReadOnlySpan.
        /// This overload avoids memory allocations and provides optimal performance.
        /// </summary>
        /// <param name="fileContent">File content as ReadOnlySpan of bytes.</param>
        /// <returns>True if the provided file is an archive; otherwise, false. Supported archive types include: ZIP, RAR, 7-Zip, TAR, GZIP, BZIP2, LZIP, and XZ.</returns>
        public static bool IsArchive(System.ReadOnlySpan<byte> fileContent)
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

        internal static IFileType FindBestMatch(System.ReadOnlySpan<byte> content)
        {
            var matches = new List<IFileType>();
            
            foreach (var fileType in Types)
            {
                if (fileType.DoesMatchWith(content))
                {
                    matches.Add(fileType);
                }
            }

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

        private static IFileType FindBestMatch(System.ReadOnlySpan<byte> content, IList<IFileType> result)
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
                if (result.ElementAt(typeIndex) is not IFileType currentType)
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
                if (result.ElementAt(typeIndex) is not IFileType currentType)
                    throw new TypeNotFoundException();
                
                var currentScore = currentType.GetMatchingNumber(content);

                scoreboard.Add(new MatchScore(currentType, currentScore));
            }

            return scoreboard;
        }

        private static IList<MatchScore> CreateScoreboard(System.ReadOnlySpan<byte> content, IList<IFileType> result)
        {
            var scoreboard = new List<MatchScore>();

            for (var typeIndex = 0; typeIndex < result.Count(); typeIndex++)
            {
                if (result.ElementAt(typeIndex) is not IFileType currentType)
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

        private static IFileType ReturnBestMatch(System.ReadOnlySpan<byte> content, IList<IFileType> matches)
            => matches.Any()
                ? matches.Count() == 1
                    ? matches.First()
                    : FindBestMatch(content, matches)
                : null;
        
        internal static async Task<IFileType> FindBestMatchAsync(Stream fileContent, CancellationToken cancellationToken = default)
        {
            var matches = new List<IFileType>();
            
            foreach (var fileType in Types)
            {
                if (await fileType.DoesMatchWithAsync(fileContent, resetPosition: true).ConfigureAwait(false))
                {
                    matches.Add(fileType);
                }
            }

            if (!matches.Any())
            {
                throw new TypeNotFoundException();
            }

            return await ReturnBestMatchAsync(fileContent, matches, cancellationToken).ConfigureAwait(false);
        }

        private static async Task<IFileType> FindBestMatchAsync(Stream fileContent, IList<IFileType> result, CancellationToken cancellationToken = default)
        {
            try
            {
                var scoreboard = await CreateScoreboardAsync(fileContent, result, cancellationToken).ConfigureAwait(false);
                return FindMaxScore(scoreboard);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private static async Task<IList<MatchScore>> CreateScoreboardAsync(Stream fileContent, IList<IFileType> result, CancellationToken cancellationToken = default)
        {
            var scoreboard = new List<MatchScore>();

            for (var typeIndex = 0; typeIndex < result.Count(); typeIndex++)
            {
                if (result.ElementAt(typeIndex) is not IFileType currentType)
                    throw new TypeNotFoundException();
                    
                var currentScore = await GetMatchingNumberAsync(currentType, fileContent, cancellationToken).ConfigureAwait(false);

                scoreboard.Add(new MatchScore(currentType, currentScore));
            }

            return scoreboard;
        }

        private static async Task<int> GetMatchingNumberAsync(IFileType fileType, Stream stream, CancellationToken cancellationToken = default)
        {
            var buffer = new byte[Math.Max(fileType.MaxMagicSequenceLength, 20)];
            stream.Position = 0;
            await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);

            return fileType.GetMatchingNumber(buffer);
        }

        private static async Task<IFileType> ReturnBestMatchAsync(Stream fileContent, IList<IFileType> matches, CancellationToken cancellationToken = default)
            => matches.Any()
                ? matches.Count() == 1
                    ? matches.First()
                    : await FindBestMatchAsync(fileContent, matches, cancellationToken).ConfigureAwait(false)
                : null;
    }
}