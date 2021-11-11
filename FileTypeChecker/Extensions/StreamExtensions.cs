namespace FileTypeChecker.Extensions
{
    using FileTypeChecker.Abstracts;
    using FileTypeChecker.Types;
    using System.IO;
    using System.Threading.Tasks;

    public static class StreamExtensions
    {
        /// <summary>
        /// Validates that the file is from certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="fileContent">File as stream</param>
        /// <returns>True if file match the desired type otherwise returns false.</returns>
        public static bool Is<T>(this Stream fileContent) where T : FileType, IFileType, new()
        {
            var instance = new T();
            var match = FileTypeValidator.GetBestMatch(fileContent);

            return match.GetType() == instance.GetType();
        }

        /// <summary>
        /// Validates that the file is from certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="fileContent">File as stream</param>
        /// <returns>True if file match the desired type otherwise returns false.</returns>
        public static async Task<bool> IsAsync<T>(this Stream fileContent) where T : FileType, IFileType, new()
        {
            var instance = new T();
            var match = await FileTypeValidator.GetBestMatchAsync(fileContent);

            return match.GetType() == instance.GetType();
        }

        /// <summary>
        /// Validates that the current file is image.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>Returns true if the provided file is image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF, PNG, and TIF.</returns>
        public static bool IsImage(this Stream fileContent)
            => fileContent.Is<Bitmap>()
            || fileContent.Is<JointPhotographicExpertsGroup>()
            || fileContent.Is<GraphicsInterchangeFormat87>()
            || fileContent.Is<GraphicsInterchangeFormat89>()
            || fileContent.Is<PortableNetworkGraphic>()
            || fileContent.Is<TaggedImageFileFormat>();

        /// <summary>
        /// Validates that the current file is image.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>Returns true if the provided file is image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF, PNG, and TIF.</returns>
        public static async Task<bool> IsImageAsync(this Stream fileContent)
           => await fileContent.IsAsync<Bitmap>().ConfigureAwait(false)
           || await fileContent.IsAsync<JointPhotographicExpertsGroup>().ConfigureAwait(false)
           || await fileContent.IsAsync<GraphicsInterchangeFormat87>().ConfigureAwait(false)
           || await fileContent.IsAsync<GraphicsInterchangeFormat89>().ConfigureAwait(false)
           || await fileContent.IsAsync<PortableNetworkGraphic>().ConfigureAwait(false)
           || await fileContent.IsAsync<TaggedImageFileFormat>().ConfigureAwait(false);

        /// <summary>
        /// Validates that the current file is archive.
        /// </summary>
        /// <param name="fileContent"File to check as stream.></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar, Zip, BZip2, LZip, and Xz.</returns>
        public static bool IsArchive(this Stream fileContent)
            => fileContent.Is<ExtensibleArchive>()
            || fileContent.Is<Gzip>()
            || fileContent.Is<RarArchive>()
            || fileContent.Is<SevenZipFile>()
            || fileContent.Is<TarArchive>()
            || fileContent.Is<ZipFile>()
            || fileContent.Is<BZip2File>()
            || fileContent.Is<LZipFile>()
            || fileContent.Is<XzFile>();

        /// <summary>
        /// Validates that the current file is archive.
        /// </summary>
        /// <param name="fileContent"File to check as stream.></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar, Zip, BZip2, LZip, and Xz.</returns>
        public static async Task<bool> IsArchiveAsync(this Stream fileContent)
           => await fileContent.IsAsync<ExtensibleArchive>().ConfigureAwait(false)
           || await fileContent.IsAsync<Gzip>().ConfigureAwait(false)
           || await fileContent.IsAsync<RarArchive>().ConfigureAwait(false)
           || await fileContent.IsAsync<SevenZipFile>().ConfigureAwait(false)
           || await fileContent.IsAsync<TarArchive>().ConfigureAwait(false)
           || await fileContent.IsAsync<ZipFile>().ConfigureAwait(false)
           || await fileContent.IsAsync<BZip2File>().ConfigureAwait(false)
           || await fileContent.IsAsync<LZipFile>().ConfigureAwait(false)
           || await fileContent.IsAsync<XzFile>().ConfigureAwait(false);

        /// <summary>
        /// Validates taht the current file is executable or executable and linkable.
        /// </summary>
        /// <param name="fileContent">Returns true if the provided file is an executable otherwise returns false.</param>
        /// <returns></returns>
        public static bool IsExecutable(this Stream fileContent)
            => fileContent.Is<Executable>()
            || fileContent.Is<ExecutableAndLinkableFormat>();

        /// <summary>
        /// Validates taht the current file is executable or executable and linkable.
        /// </summary>
        /// <param name="fileContent">Returns true if the provided file is an executable otherwise returns false.</param>
        /// <returns></returns>
        public static async Task<bool> IsExecutableAsync(this Stream fileContent)
          => await fileContent.IsAsync<Executable>().ConfigureAwait(false)
          || await fileContent.IsAsync<ExecutableAndLinkableFormat>().ConfigureAwait(false);

        /// <summary>
        /// Validates that the current file is a document.
        /// </summary>
        /// <param name="fileContent"File to check as stream.></param>
        /// <returns>Returns true if the provided file is a document otherwise returns false. Supported document types are: Extensible Markup Language, Microsoft Office365 Document, Microsoft Office Document, Portable Document Format.</returns>
        public static bool IsDocument(this Stream fileContent)
            => fileContent.Is<ExtensibleMarkupLanguage>()
            || fileContent.Is<MicrosoftOffice365Document>()
            || fileContent.Is<MicrosoftOfficeDocument>()
            || fileContent.Is<PortableDocumentFormat>();

        /// <summary>
        /// Validates that the current file is a document.
        /// </summary>
        /// <param name="fileContent"File to check as stream.></param>
        /// <returns>Returns true if the provided file is a document otherwise returns false. Supported document types are: Extensible Markup Language, Microsoft Office365 Document, Microsoft Office Document, Portable Document Format.</returns>
        public static async Task<bool> IsDocumentAsync(this Stream fileContent)
            => await fileContent.IsAsync<ExtensibleMarkupLanguage>().ConfigureAwait(false)
            || await fileContent.IsAsync<MicrosoftOffice365Document>().ConfigureAwait(false)
            || await fileContent.IsAsync<MicrosoftOfficeDocument>()
            || await fileContent.IsAsync<PortableDocumentFormat>();
    }
}
