namespace FileTypeChecker.Types
{
    using Abstracts;

    public class HyperTextMarkupLanguage : FileType, IFileType
    {
        public const string TypeName = "HyperText Markup Language";
        public const string TypeMimeType = "text/html";
        public const string TypeExtension = "html";
        private static readonly byte[][] MagicBytes = {
            new byte[] { 0x3C, 0x21, 0x64, 0x6F, 0x63, 0x74, 0x79, 0x70 },
            new byte[] { 0x3C, 0x21, 0x44, 0x4F, 0x43, 0x54, 0x59, 0x50 }
        };

        public HyperTextMarkupLanguage() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
