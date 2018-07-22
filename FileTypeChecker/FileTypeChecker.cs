namespace FileTypeChecker
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileTypeChecker : IFileTypeChecker
    {
        private static readonly IList<IFileType> knownFileTypes = new List<IFileType>
        {
            new FileType("Bitmap", ".bmp", new FileTypeMatcher( new byte[] {0x42, 0x4d})),
            new FileType("Portable Network Graphic", ".png",
                new FileTypeMatcher(new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A})),
            new FileType("Graphics Interchange Format 87a", ".gif",
                new FileTypeMatcher(new byte[] {0x47, 0x49, 0x46, 0x38, 0x37, 0x61})),
            new FileType("Graphics Interchange Format 89a", ".gif",
                new FileTypeMatcher(new byte[] {0x47, 0x49, 0x46, 0x38, 0x39, 0x61})),
            new FileType("Portable Document Format", ".pdf", new FileTypeMatcher(new byte[] { 0x25, 0x50, 0x44, 0x46 })),
            new FileType("eXtensible Markup Language", ".xml", new FileTypeMatcher( new byte[]{0x3c, 0x3f, 0x78, 0x6d, 0x6c, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31 })),
            new FileType("Comma-separated Values", ".csv", new FileTypeMatcher(new byte[] { 0x4D, 0x65, 0x74, 0x65, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x50 }))
            // ... Potentially more in future
        };

        public IFileType GetFileType(Stream fileContent)
        {
            return GetFileTypes(fileContent).FirstOrDefault() ?? FileType.Unknown;
        }

        public IEnumerable<IFileType> GetFileTypes(Stream stream)
        {
            return knownFileTypes.Where(fileType => fileType.Matches(stream));
        }
    }
}