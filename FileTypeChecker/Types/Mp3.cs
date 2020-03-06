namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;
    internal class Mp3 : FileType, IFileType
    {
        private static readonly string name = "MP3 file";
        private static readonly string extension = "mp3";
        private static readonly byte[][] magicBytesJaggedArray = { new byte[] { 0xFF, 0xFB }, new byte[] { 0x49, 0x44, 0x33 } };

        public Mp3() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
