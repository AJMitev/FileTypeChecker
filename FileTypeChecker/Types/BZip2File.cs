namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class BZip2File : FileType, IFileType
    {
        private const string name = "BZIP2 file";
        private const string extension = "bz2";
        private static readonly byte[][] magicBytesJaggedArray = { new [] { (byte)'B', (byte)'Z' } };

        public BZip2File() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
