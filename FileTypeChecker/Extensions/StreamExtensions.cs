namespace FileTypeChecker.Extensions
{
    using Abstracts;
    using Types;
    using System.IO;

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
            var isRecognizable = FileTypeValidator.IsTypeRecognizable(fileContent);

            if (!isRecognizable)
            {
                return false;
            }

            var match = FileTypeValidator.FindBestMatch(fileContent);

            return match?.GetType() == instance.GetType();
        }

        /// <summary>
        /// Validates that the current file is image.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>Returns true if the provided file is image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF, PNG, and TIF.</returns>
        public static bool IsImage(this Stream fileContent)
            => fileContent.Is<Bitmap>()
            || fileContent.Is<Webp>()
            || fileContent.Is<JointPhotographicExpertsGroup>()
            || fileContent.Is<GraphicsInterchangeFormat87>()
            || fileContent.Is<GraphicsInterchangeFormat89>()
            || fileContent.Is<PortableNetworkGraphic>()
            || fileContent.Is<TaggedImageFileFormat>();

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
        /// Validates taht the current file is executable or executable and linkable.
        /// </summary>
        /// <param name="fileContent">Returns true if the provided file is an executable otherwise returns false.</param>
        /// <returns></returns>
        public static bool IsExecutable(this Stream fileContent)
            => fileContent.Is<Executable>()
            || fileContent.Is<ExecutableAndLinkableFormat>();

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
    }
}
