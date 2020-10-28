namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class PhotoshopDocumentFile : FileType, IFileType
    {
        private const string name = "Photoshop Document file";
        private const string extension = FileExtension.Psd;
        private static readonly byte[] magicBytes = new byte[] { 0x38, 0x42, 0x50, 0x53};

        public PhotoshopDocumentFile() : base(name, extension, magicBytes)
        {
        }
    }
}
