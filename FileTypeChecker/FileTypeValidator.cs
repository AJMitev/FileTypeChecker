namespace FileTypeChecker
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Abstracts;

    /// <summary>
    /// Provides you a method for file type validation.
    /// </summary>
    public static class FileTypeValidator
    {
        /// <summary>
        /// Checks that the particular type is supported.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>If current type is supported</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.A rgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        public static bool IsTypeRecognizable(Stream fileContent)
        {
            return knownFileTypes.Any(type => type.DoesMatchWith(fileContent));
        }

        /// <summary>
        /// Get details about current file type.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>Instance of <see cref="IFileType}"/> type.</returns>
        /// /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        public static IFileType GetFileType(Stream fileContent)
        {
            return knownFileTypes.SingleOrDefault(fileType => fileType.DoesMatchWith(fileContent));
        }
    }
}