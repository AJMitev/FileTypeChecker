namespace FileTypeChecker.Types
{
    using Abstracts;

    public class PortableDocumentFormat : FileType, IFileType
    {
        public const string TypeName = "Portable Document Format";
        public const string TypeMimeType = "application/pdf";
        public const string TypeExtension = "pdf";
        private static readonly byte[] MagicBytes = { 0x25, 0x50, 0x44, 0x46, 0x2D };

        public PortableDocumentFormat() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
