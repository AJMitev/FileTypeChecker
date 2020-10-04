namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class DiskImageFile : FileType, IFileType
    {
        private const string name = "ISO9660 CD/DVD image file";
        private const string extension = "iso";
        private static readonly byte[] magicBytes = new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 };

        public DiskImageFile() : base(name, extension, magicBytes)
        {
        }
    }
}
