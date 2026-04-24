namespace FileTypeChecker.Types
{
    using Abstracts;

    /// <summary>
    /// Free Lossless Audio Codec file. Identified by the fLaC magic marker.
    /// </summary>
    public class Flac : FileType, IFileType
    {
        public const string TypeName = "FLAC audio file";
        public const string TypeMimeType = "audio/flac";
        public const string TypeExtension = "flac";
        private static readonly byte[] MagicBytes = { 0x66, 0x4C, 0x61, 0x43 }; // fLaC

        public Flac() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
