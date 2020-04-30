namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class LZipFile : FileType, IFileType
    {
        private static readonly string name = "LZIP file";
        private static readonly string extension = "lz";
        private static readonly byte[][] magicBytesJaggedArray = { new [] { (byte)'L', (byte)'Z', (byte)'I', (byte)'P', (byte)1 } };

        public LZipFile() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
