namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Mpeg : FileType, IFileType
    {
        private const string name = "MPEG audio file frame synch pattern";
        private const string extension = FileExtension.Mp3;
        private static readonly byte[][] magicBytesJaggedArray =
        {
            new byte[] { 0xFF, 0xE3 },
            new byte[] { 0xFF, 0xF2 },
            new byte[] { 0xFF, 0xF3 },
            new byte[] { 0xFF, 0xFB }
        };


        public Mpeg() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
