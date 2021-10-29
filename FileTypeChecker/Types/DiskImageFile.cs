namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class DiskImageFile : FileType, IFileType
    {
        public const string TypeName = "ISO9660 CD/DVD image file";
        public const string TypeExtension = "iso";
        private static readonly byte[] magicBytes = new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 };

        public DiskImageFile() : base(TypeName, TypeExtension, magicBytes)
        {
        }
    }
}
