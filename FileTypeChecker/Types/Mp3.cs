namespace FileTypeChecker.Types
{
    using Abstracts;
    public class Mp3 : FileType, IFileType
    {
        public const string TypeName = "MPEG audio file frame synch pattern";
        public const string TypeMimeType = "audio/mpeg";
        public const string TypeExtension = "mp3";
        private static readonly MagicSequence MagicBytes = new(new byte[] { 0x49, 0x44, 0x33 });

        public Mp3() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
