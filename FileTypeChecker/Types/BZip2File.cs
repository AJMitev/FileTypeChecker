namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class BZip2File : FileType, IFileType
    {
        private static readonly string name = "BZIP2 file";
        private static readonly string extension = "bz2";
        private static readonly byte[][] magicBytesJaggedArray = { new [] { (byte)'B', (byte)'Z' } };

        public BZip2File() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
