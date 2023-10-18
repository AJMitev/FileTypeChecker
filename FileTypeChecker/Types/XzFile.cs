namespace FileTypeChecker.Types
{
    using Abstracts;

    public class XzFile : FileType, IFileType
    {
        public const string TypeName = "XZ file";
        public const string TypeExtension = "xz";
        private static readonly MagicSequence MagicBytes =   new(new byte[] { 0xFD, 0x37, 0x7A, 0x58, 0x5a, 0x00 });

        public XzFile() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
