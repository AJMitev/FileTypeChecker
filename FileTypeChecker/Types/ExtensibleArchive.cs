namespace FileTypeChecker.Types
{
    using Abstracts;

    public class ExtensibleArchive : FileType, IFileType
    {
        public const string TypeName = "eXtensible ARchive format";
        public const string TypeMimeType = "application/x-xar";
        public const string TypeExtension = "xar";
        private static readonly byte[] MagicBytes = { 0x78, 0x61, 0x72, 0x21 };

        public ExtensibleArchive() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
