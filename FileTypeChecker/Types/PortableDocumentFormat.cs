namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class PortableDocumentFormat : FileType, IFileType
    {
        private const string name = "Portable Document Format";
        private const string extension = FileExtension.Pdf;
        private static readonly byte[] magicBytes = new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D };

        public PortableDocumentFormat() : base(name, extension, magicBytes)
        {
        }
    }
}
