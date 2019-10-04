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
            new FileType("Portable Document Format", "pdf", new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D }),
            new FileType("eXtensible Markup Language", "xml", new byte[]{0x3c, 0x3f, 0x78, 0x6d, 0x6c, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31 }),
            new FileType("Comma-separated Values", "csv", new byte[] { 0x4D, 0x65, 0x74, 0x65, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x50 }),
            new FileType("RAR archive version 1.50", "rar", new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00}),
            new FileType("RAR archive version 5.00", "rar", new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00}),
            new FileType("Photoshop Document file", "psd", new byte[] { 0x38, 0x42, 0x50, 0x53})
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