namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class FlexibleImageTransportSystem : FileType, IFileType
    {
        private const string name = "Flexible Image Transport System";
        private const string extension = FileExtension.Fits;
        private static readonly byte[] magicBytes = new byte[] { 0x53, 0x49, 0x4d, 0x50, 0x4c, 0x45 };

        public FlexibleImageTransportSystem() : base(name, extension, magicBytes)
        {
        }
    }
}
