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
        private static readonly IList<IFileType> knownFileTypes = new List<IFileType>
        {
            new FileType("Bitmap", "bmp", new byte[] {0x42, 0x4d}),
            new FileType("JPEG", "jpg",  new byte[] {0xFF, 0xD8,0xFF}),
            new FileType("Portable Network Graphic", "png",new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A}),
            new FileType("Graphics Interchange Format 87a", "gif",new byte[] {0x47, 0x49, 0x46, 0x38, 0x37, 0x61}),
            new FileType("Graphics Interchange Format 89a", "gif",new byte[] {0x47, 0x49, 0x46, 0x38, 0x39, 0x61}),
            new FileType("Tagged Image File Format", "tif", new byte[] { 0x49, 0x49, 0x2A, 0x00 }),
            new FileType("Tagged Image File Format", "tiff", new byte[] { 0x4D, 0x4D, 0x00, 0x2A }),
            new FileType("Portable Document Format", "pdf", new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D }),
            new FileType("eXtensible Markup Language", "xml", new byte[]{0x3c, 0x3f, 0x78, 0x6d, 0x6c, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31 }),
            new FileType("Comma-separated Values", "csv", new byte[] { 0x4D, 0x65, 0x74, 0x65, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x50 }),
            new FileType("RAR archive version 1.50", "rar", new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00}),
            new FileType("RAR archive version 5.00", "rar", new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00}),
            new FileType("Photoshop Document file", "psd", new byte[] { 0x38, 0x42, 0x50, 0x53}),
            new FileType("ZIP file", "zip", new byte[] { 0x50, 0x4B, 0x03, 0x04}),
            new FileType("ZIP file (empty)", "zip", new byte[] { 0x50, 0x4B, 0x05, 0x06}),
            new FileType("ZIP file (spanned)", "zip", new byte[] { 0x50, 0x4B, 0x07, 0x08}),
            new FileType("eXtensible ARchive format", "xar", new byte[] { 0x78, 0x61, 0x72, 0x21}),
            new FileType("TAR Archive", "tar", new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72}),
            new FileType("7-Zip File Format", "7z", new byte[] { 0x1F,0x8B}),
            new FileType("GZIP compressed file", "gz", new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72}),
            new FileType("DOS MZ executable", "exe", new byte[] { 0x4D, 0x5A}),
            new FileType("Executable and Linkable Format", "elf", new byte[] { 0x7F, 0x45, 0x4C, 0x46}),
            new FileType("Audio Video Interleave video format", "avi", new byte[] { 0x52, 0x49, 0x46, 0x46 }),
            new FileType("MPEG-1 Layer 3 file", "mp3", new byte[] { 0xFF, 0xFB }),
            new FileType("MP3 file with an ID3v2 container", "mp3", new byte[] { 0x49, 0x44, 0x33 }),
            new FileType("ISO9660 CD/DVD image file", "iso", new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 }),
            new FileType("Microsoft Office Document 97-2003", "doc", new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }),
        };

        /// <summary>
        /// Checks that the particular type is supported.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>If current type is supported</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        public static bool IsTypeRecognizable(FileStream fileContent)
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
        public static IFileType GetFileType(FileStream fileContent)
        {
            return knownFileTypes.SingleOrDefault(fileType => fileType.DoesMatchWith(fileContent));
        }
    }
}