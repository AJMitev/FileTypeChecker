namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class PhotoshopDocumentFile : FileType, IFileType
    {
        private static readonly string name = "Photoshop Document file";
        private static readonly string extension = "psd";
        private static readonly byte[] magicBytes = new byte[] { 0x38, 0x42, 0x50, 0x53};

        public PhotoshopDocumentFile() : base(name, extension, magicBytes)
        {
        }
    }
}
