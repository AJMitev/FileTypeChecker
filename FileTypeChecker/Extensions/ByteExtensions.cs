namespace FileTypeChecker.Extensions
{
    using FileTypeChecker.Abstracts;
    using FileTypeChecker.Types;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class ByteExtensions
    {
        public static bool SequenceEqual(this IList<byte> thisArr, IList<byte> otherArr)
        {
            var lenght = Math.Min(thisArr.Count, otherArr.Count);

            for (int i = 0; i < lenght; i++)
            {
                var thisByte = thisArr[i];
                var otherByte = otherArr[i];

                if (thisByte != otherByte)
                {
                    return false;
                }
            }

            return true;
        }

        public static int CountMatchingBytes(this IList<byte> thisArr, IList<byte> otherArr)
        {
            var lenght = Math.Min(thisArr.Count, otherArr.Count);
            var counter = 0;

            for (int i = 0; i < lenght; i++)
            {
                var thisByte = thisArr[i];
                var otherByte = otherArr[i];

                if (thisByte != otherByte)
                {
                    return counter;
                }

                counter++;
            }

            return counter;
        }

        /// <summary>
        /// Validates that the file is from certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="fileContent">File as stream</param>
        /// <returns>True if file match the desired type otherwise returns false.</returns>
        public static bool Is<T>(this byte[] fileContent) where T : FileType, IFileType, new()
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
        public static bool IsImage(this byte[] fileContent)
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
        /// <param name="content"File to check as stream.></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar, Zip, BZip2, LZip, and Xz.</returns>
        public static bool IsArchive(this byte[] content)
            => content.Is<ExtensibleArchive>()
            || content.Is<Gzip>()
            || content.Is<RarArchive>()
            || content.Is<SevenZipFile>()
            || content.Is<TarArchive>()
            || content.Is<ZipFile>()
            || content.Is<BZip2File>()
            || content.Is<LZipFile>()
            || content.Is<XzFile>();

        /// <summary>
        /// Validates taht the current file is executable or executable and linkable.
        /// </summary>
        /// <param name="content">Returns true if the provided file is an executable otherwise returns false.</param>
        /// <returns></returns>
        public static bool IsExecutable(this byte[] content)
            => content.Is<Executable>()
            || content.Is<ExecutableAndLinkableFormat>();

        /// <summary>
        /// Validates that the current file is a document.
        /// </summary>
        /// <param name="content"File to check as stream.></param>
        /// <returns>Returns true if the provided file is a document otherwise returns false. Supported document types are: Extensible Markup Language, Microsoft Office365 Document, Microsoft Office Document, Portable Document Format.</returns>
        public static bool IsDocument(this byte[] content)
            => content.Is<ExtensibleMarkupLanguage>()
            || content.Is<MicrosoftOffice365Document>()
            || content.Is<MicrosoftOfficeDocument>()
            || content.Is<PortableDocumentFormat>();
    }
}
