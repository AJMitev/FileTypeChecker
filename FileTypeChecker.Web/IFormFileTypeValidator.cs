namespace FileTypeChecker.Web
{
    using FileTypeChecker.Abstracts;
    using FileTypeChecker.Common;
    using FileTypeChecker.Extensions;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using FileTypeChecker.Web;
    using System.Collections.Generic;

    public static class IFormFileTypeValidator
    {
        /// <summary>
        /// Checks that the particular type is supported.
        /// </summary>
        /// <param name="formFile">Object that implements IFormFile interface.</param>
        /// <returns>If current type is supported</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>

        public static bool IsTypeRecognizable(IFormFile formFile)
        {
            DataValidator.ThrowIfNull(formFile, nameof(IFormFile));
            var stream = formFile.ReadFileAsStream();

            return FileTypeValidator.IsTypeRecognizable(stream);
        }

        /// <summary>
        /// Checks that the particular type is supported.
        /// </summary>
        /// <param name="formFile">Object that implements IFormFile interface.</param>
        /// <returns>If current type is supported</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>

        public static bool IsTypeRecognizable(IEnumerable<IFormFile> formFiles)
        {
            DataValidator.ThrowIfNull(formFiles, nameof(IEnumerable<IFormFile>));

            foreach (var formFile in formFiles)
            {
                var stream = formFile.ReadFileAsStream();

                if (!FileTypeValidator.IsTypeRecognizable(stream))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Get details about current file type.
        /// </summary>
        /// <param name="formFile">Object that implements IFormFile interface.</param>
        /// <returns>Instance of <see cref="IFileType}"/> type.</returns>
        /// /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static IFileType GetFileType(IFormFile formFile)
        {
            DataValidator.ThrowIfNull(formFile, nameof(Stream));

            return FileTypeValidator.GetFileType(formFile.ReadFileAsStream());
        }

        /// <summary>
        /// Validates that the file is from certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="formFile">Object that implements IFormFile interface.</param>
        /// <returns>True if file match the desired type otherwise returns false.</returns>
        public static bool Is<T>(IFormFile formFile) where T : FileType, IFileType, new()
            => formFile.ReadFileAsStream().Is<T>();
        /// <summary>
        /// Validates that the current file is image.
        /// </summary>
        /// <param name="formFile">Object that implements IFormFile interface.</param>
        /// <returns>Returns true if the provided file is image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF and PNG.</returns>
        public static bool IsImage(IFormFile formFile)
            => formFile.ReadFileAsStream().IsImage();
        /// <summary>
        /// Validates that the current file is archive.
        /// </summary>
        /// <param name="formFile">Object that implements IFormFile interface.</param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar and Zip.</returns>
        public static bool IsArchive(IFormFile formFile)
            => formFile.ReadFileAsStream().IsArchive();
    }
}
