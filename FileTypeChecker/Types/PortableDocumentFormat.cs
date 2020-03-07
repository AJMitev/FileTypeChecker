namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class PortableDocumentFormat : FileType, IFileType
    {
        private static readonly string name = "Portable Document Format";
        private static readonly string extension =  "pdf";
        private static readonly byte[] magicBytes = new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D };

        public PortableDocumentFormat() : base(name, extension, magicBytes)
        {
        }
    }
}
