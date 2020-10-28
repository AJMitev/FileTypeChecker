namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class ZipFile : FileType, IFileType
    {
        private const string name = "ZIP file";
        private const string extension = FileExtension.Zip;
        private static readonly byte[][] magicBytesJaggedArray = { new byte[] { 0x50, 0x4B, 0x03, 0x04 }, new byte[] { 0x50, 0x4B, 0x05, 0x06 }, new byte[] { 0x50, 0x4B, 0x07, 0x08 } };

        public ZipFile() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
