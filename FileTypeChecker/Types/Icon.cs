namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Icon : FileType, IFileType
    {
        public const string TypeName = "Icon";
        public const string TypeExtension = "ico";
        private static readonly byte[] magicBytes = new byte[] { 0x00, 0x00, 0x01, 0x00 };

        public Icon() : base(TypeName, TypeExtension, magicBytes) { }
    }
}
