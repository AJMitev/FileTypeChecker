namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class PortableDocumentFormat : FileType, IFileType
    {
        public const string TypeName = "Portable Document Format";
        public const string TypeExtension = "pdf";
        private static readonly byte[] magicBytes = new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D };

        public PortableDocumentFormat() : base(TypeName, TypeExtension, magicBytes)
        {
        }
    }
}
