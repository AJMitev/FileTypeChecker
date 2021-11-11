namespace FileTypeChecker.Web
{
    using FileTypeChecker.Abstracts;
    using FileTypeChecker.Extensions;
    using Microsoft.AspNetCore.Http;
    using System.IO;

    public static class s
    {
        /// <summary>
        /// Validates that the file is from certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="formFile"></param>
        /// <returns>True if file match the desired type otherwise returns false.</returns>
        public static bool Is<T>(this IFormFile formFile) where T : FileType, IFileType, new()
        {
            var instance = new T();
            return instance.DoesMatchWith(formFile.ReadFileAsStream());
        }
        /// <summary>
        /// Validates that the current file is image.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns>Returns true if the provided file is image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF, PNG, and TIF.</returns>
        public static bool IsImage(this IFormFile formFile)
            => formFile.ReadFileAsStream().IsImage();

        /// <summary>
        /// Validates that the current file is archive.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar, Zip, BZip2, LZip, and Xz.</returns>
        public static bool IsArchive(this IFormFile formFile)
            => formFile.ReadFileAsStream().IsArchive();

        /// <summary>
        /// Validates taht the current file is executable or executable and linkable.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static bool IsExecutable(this IFormFile formFile)
            => formFile.ReadFileAsStream().IsExecutable();

        /// <summary>
        /// Validates taht the current file is executable or executable and linkable.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static bool IsDocument(this IFormFile formFile)
            => formFile.ReadFileAsStream().IsDocument();

        /// <summary>
        /// Transform object that implements IFormFile interface to MemoryStream.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns>Returns object of type MemoryStream.</returns>
        public static MemoryStream ReadFileAsStream(this IFormFile formFile)
        {
            var stream = new MemoryStream();
            formFile.CopyTo(stream);
            return stream;
        }
    }
}
