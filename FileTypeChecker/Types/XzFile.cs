namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class XzFile : FileType, IFileType
    {
        private const string name = "XZ file";
        private const string extension = "xz";
        private static readonly byte[][] magicBytesJaggedArray = {  new byte[] { 0xFD, 0x37, 0x7A, 0x58, 0x5a, 0x00 } };

        public XzFile() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
