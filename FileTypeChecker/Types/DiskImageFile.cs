namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class DiskImageFile : FileType, IFileType
    {
        private static readonly string name = "ISO9660 CD/DVD image file";
        private static readonly string extension = "iso";
        private static readonly byte[] magicBytes = new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 };

        internal DiskImageFile() : base(name, extension, magicBytes)
        {
        }
    }
}
