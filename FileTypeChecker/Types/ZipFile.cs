namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class ZipFile : FileType, IFileType
    {
        public const string TypeName = "ZIP file";
        public const string TypeExtension = "zip";
        private static readonly byte[][] magicBytesJaggedArray =
           {
            new byte[] { 0x50, 0x4B, 0x03, 0x04 },
            new byte[] { 0x50, 0x4B, 0x05, 0x06 },
            new byte[] { 0x50, 0x4B, 0x07, 0x08 }
            };

        public ZipFile() : base(TypeName, TypeExtension, magicBytesJaggedArray)
        {
        }
    }
}
