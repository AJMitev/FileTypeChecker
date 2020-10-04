namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class LZipFile : FileType, IFileType
    {
        private const string name = "LZIP file";
        private const string extension = "lz";
        private static readonly byte[][] magicBytesJaggedArray = { new [] { (byte)'L', (byte)'Z', (byte)'I', (byte)'P', (byte)1 } };

        public LZipFile() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
