namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class ExtensibleArchive : FileType, IFileType
    {
        public const string TypeName = "eXtensible ARchive format";
        public const string TypeExtension = "xar";
        private static readonly byte[] MagicBytes = { 0x78, 0x61, 0x72, 0x21 };

        public ExtensibleArchive() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
