namespace FileTypeChecker.Types
{
    using Abstracts;

    public class Icon : FileType, IFileType
    {
        public const string TypeName = "Icon";
        public const string TypeMimeType = "image/x-icon";
        public const string TypeExtension = "ico";
        private static readonly byte[] MagicBytes = { 0x00, 0x00, 0x01, 0x00 };

        public Icon() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes) { }
    }
}
