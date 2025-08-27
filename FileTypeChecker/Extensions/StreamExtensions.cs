namespace FileTypeChecker.Extensions
{
    using Abstracts;
    using Types;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public static class StreamExtensions
    {
        /// <summary>
        /// Validates that the file is of a specific type.
        /// </summary>
        /// <typeparam name="T">The file type that implements <see cref="FileType"/> and <see cref="IFileType"/>.</typeparam>
        /// <param name="fileContent">File content as a stream.</param>
        /// <returns>True if the file matches the desired type; otherwise, false.</returns>
        public static bool Is<T>(this Stream fileContent) where T : FileType, IFileType, new()
        {
            var instance = new T();
            var isRecognizable = FileTypeValidator.IsTypeRecognizable(fileContent);

            if (!isRecognizable)
            {
                return false;
            }

            var match = FileTypeValidator.FindBestMatch(fileContent);

            return match?.GetType() == instance.GetType();
        }

        /// <summary>
        /// Validates that the current file is an image.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <returns>True if the provided file is an image; otherwise, false. Supported image types include: Bitmap, JPEG, GIF, PNG, TIFF, WebP, PSD, HEIR, and ICO.</returns>
        public static bool IsImage(this Stream fileContent)
            => fileContent.Is<Bitmap>()
            || fileContent.Is<Webp>()
            || fileContent.Is<JointPhotographicExpertsGroup>()
            || fileContent.Is<GraphicsInterchangeFormat87>()
            || fileContent.Is<GraphicsInterchangeFormat89>()
            || fileContent.Is<PortableNetworkGraphic>()
            || fileContent.Is<TaggedImageFileFormat>();

        /// <summary>
        /// Validates that the current file is an archive.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <returns>True if the provided file is an archive; otherwise, false. Supported archive types include: ZIP, RAR, 7-Zip, TAR, GZIP, BZIP2, ZIP, and XZ.</returns>
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
        /// Validates that the current file is an executable.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <returns>True if the provided file is an executable; otherwise, false. Supports Windows executables and ELF binaries.</returns>
        public static bool IsExecutable(this Stream fileContent)
            => fileContent.Is<Executable>()
            || fileContent.Is<ExecutableAndLinkableFormat>();

        /// <summary>
        /// Validates that the current file is a document.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <returns>True if the provided file is a document; otherwise, false. Supported document types include: XML, Microsoft Office documents (DOC/DOCX/XLS/XLSX), and PDF.</returns>
        public static bool IsDocument(this Stream fileContent)
            => fileContent.Is<ExtensibleMarkupLanguage>()
            || fileContent.Is<MicrosoftOffice365Document>()
            || fileContent.Is<MicrosoftOfficeDocument>()
            || fileContent.Is<PortableDocumentFormat>();

        // Async Extension Methods

        /// <summary>
        /// Asynchronously validates that the file is of a specific type.
        /// </summary>
        /// <typeparam name="T">The file type that implements <see cref="FileType"/> and <see cref="IFileType"/>.</typeparam>
        /// <param name="fileContent">File content as a stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the file matches the desired type; otherwise, false.</returns>
        public static async Task<bool> IsAsync<T>(this Stream fileContent, CancellationToken cancellationToken = default) where T : FileType, IFileType, new()
        {
            var instance = new T();
            var isRecognizable = await FileTypeValidator.IsTypeRecognizableAsync(fileContent, cancellationToken).ConfigureAwait(false);

            if (!isRecognizable)
            {
                return false;
            }

            var match = await FileTypeValidator.FindBestMatchAsync(fileContent, cancellationToken).ConfigureAwait(false);

            return match?.GetType() == instance.GetType();
        }

        /// <summary>
        /// Asynchronously validates that the current file is an image.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the provided file is an image; otherwise, false. Supported image types include: Bitmap, JPEG, GIF, PNG, TIFF, WebP, PSD, HEIR, and ICO.</returns>
        public static async Task<bool> IsImageAsync(this Stream fileContent, CancellationToken cancellationToken = default)
        {
            return await fileContent.IsAsync<Bitmap>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<Webp>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<JointPhotographicExpertsGroup>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<GraphicsInterchangeFormat87>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<GraphicsInterchangeFormat89>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<PortableNetworkGraphic>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<TaggedImageFileFormat>(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously validates that the current file is an archive.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the provided file is an archive; otherwise, false. Supported archive types include: ZIP, RAR, 7-Zip, TAR, GZIP, BZIP2, LZIP, and XZ.</returns>
        public static async Task<bool> IsArchiveAsync(this Stream fileContent, CancellationToken cancellationToken = default)
        {
            return await fileContent.IsAsync<ExtensibleArchive>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<Gzip>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<RarArchive>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<SevenZipFile>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<TarArchive>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<ZipFile>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<BZip2File>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<LZipFile>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<XzFile>(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously validates that the current file is an executable.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the provided file is an executable; otherwise, false. Supports Windows executables and ELF binaries.</returns>
        public static async Task<bool> IsExecutableAsync(this Stream fileContent, CancellationToken cancellationToken = default)
        {
            return await fileContent.IsAsync<Executable>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<ExecutableAndLinkableFormat>(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously validates that the current file is a document.
        /// </summary>
        /// <param name="fileContent">File to check as a stream.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the provided file is a document; otherwise, false. Supported document types include: XML, Microsoft Office documents (DOC/DOCX/XLS/XLSX), and PDF.</returns>
        public static async Task<bool> IsDocumentAsync(this Stream fileContent, CancellationToken cancellationToken = default)
        {
            return await fileContent.IsAsync<ExtensibleMarkupLanguage>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<MicrosoftOffice365Document>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<MicrosoftOfficeDocument>(cancellationToken).ConfigureAwait(false)
            || await fileContent.IsAsync<PortableDocumentFormat>(cancellationToken).ConfigureAwait(false);
        }
    }
}
