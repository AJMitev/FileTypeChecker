namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class ZipFile : FileType, IFileType
    {
        private static readonly string name = "ZIP file";
        private static readonly string extension = "zip";
        private static readonly byte[][] magicBytesJaggedArray = { new byte[] { 0x50, 0x4B, 0x03, 0x04 }, new byte[] { 0x50, 0x4B, 0x05, 0x06 }, new byte[] { 0x50, 0x4B, 0x07, 0x08 } };

        internal ZipFile() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
