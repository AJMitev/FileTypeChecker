namespace FileTypeChecker.Types
{
    using FileTypeChecker;
    using Abstracts;

    public class MpegAudio : FileType, IFileType
    {
        public const string TypeName = "MPEG audio file frame synch pattern";
        public const string TypeMimeType = "audio/mpeg";
        public const string TypeExtension = "mp3";
        private static readonly MagicSequence[] MagicBytes =
        {
           new(new byte[] { 0xFF, 0xE3 }),
           new(new byte[] { 0xFF, 0xF2 }),
           new(new byte[] { 0xFF, 0xF3 }),
           new(new byte[] { 0xFF, 0xFB })
        };

        public MpegAudio() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes) { }
    }
}
