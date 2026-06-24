namespace FileTypeChecker.Types
{
    using Abstracts;

    /// <summary>
    /// Ogg container file (Vorbis, Opus, FLAC). Identified by the OggS capture pattern.
    /// </summary>
    public class Ogg : FileType, IFileType
    {
        public const string TypeName = "Ogg audio file";
        public const string TypeMimeType = "audio/ogg";
        public const string TypeExtension = "ogg";
        private static readonly byte[] MagicBytes = { 0x4F, 0x67, 0x67, 0x53 }; // OggS

        public Ogg() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
