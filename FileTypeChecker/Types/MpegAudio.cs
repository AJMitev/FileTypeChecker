namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class MpegAudio : FileType, IFileType
    {
        public const string TypeName = "MPEG audio file frame synch pattern";
        public const string TypeExtension = "mp3";
        private static readonly byte[][] magicBytesJaggedArray =
        {
            new byte[] { 0xFF, 0xE3 },
            new byte[] { 0xFF, 0xF2 },
            new byte[] { 0xFF, 0xF3 },
            new byte[] { 0xFF, 0xFB }
        };


        public MpegAudio() : base(TypeName, TypeExtension, magicBytesJaggedArray)
        {
        }
    }
}
