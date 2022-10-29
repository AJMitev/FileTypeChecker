namespace FileTypeChecker.Types
{
    using FileTypeChecker;
    using FileTypeChecker.Abstracts;

    public class MpegAudio : FileType, IFileType
    {
        public const string TypeName = "MPEG audio file frame synch pattern";
        public const string TypeExtension = "mp3";
        private static readonly MagicSequence[] magicBytes =
        {
           new(new byte[] { 0xFF, 0xE3 }),
           new(new byte[] { 0xFF, 0xF2 }),
           new(new byte[] { 0xFF, 0xF3 }),
           new(new byte[] { 0xFF, 0xFB })
        };

        public MpegAudio() : base(TypeName, TypeExtension, magicBytes) { }
    }
}
