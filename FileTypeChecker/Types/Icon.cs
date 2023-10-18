namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Icon : FileType, IFileType
    {
        public const string TypeName = "Icon";
        public const string TypeExtension = "ico";
        private static readonly byte[] MagicBytes = { 0x00, 0x00, 0x01, 0x00 };

        public Icon() : base(TypeName, TypeExtension, MagicBytes) { }
    }
}
