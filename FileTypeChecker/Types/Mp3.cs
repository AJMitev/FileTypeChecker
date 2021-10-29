namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;
    public class Mp3 : FileType, IFileType
    {
        public const string TypeName = "MPEG audio file frame synch pattern";
        public const string TypeExtension = "mp3";
        private static readonly byte[][] magicBytesJaggedArray = {new byte[] { 0x49, 0x44, 0x33 } };

        public Mp3() : base(TypeName, TypeExtension, magicBytesJaggedArray)
        {
        }
    }
}
