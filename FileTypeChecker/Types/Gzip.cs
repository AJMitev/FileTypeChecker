namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Gzip : FileType, IFileType
    {
        private const string name = "GZIP compressed file";
        private const string extension = FileExtension.Gz;
        private static readonly byte[][] magicBytesJaggedArray = { new byte[] { 0x1F, 0x8B, 8 }, new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72 } };

        public Gzip() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
