namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class PhotoshopDocumentFile : FileType, IFileType
    {
        public const string TypeName = "Photoshop Document file";
        public const string TypeExtension = "psd";
        private static readonly byte[] MagicBytes = { 0x38, 0x42, 0x50, 0x53};

        public PhotoshopDocumentFile() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
