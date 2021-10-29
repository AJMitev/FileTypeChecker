namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class AudioVideoInterleaveVideoFormat : FileType, IFileType
    {
        public const string TypeName = "Audio Video Interleave video format";
        public const string TypeExtension = "avi";
        private static readonly byte[] magicBytes = new byte[] { 0x52, 0x49, 0x46, 0x46 };

        public AudioVideoInterleaveVideoFormat() : base(TypeName, TypeExtension, magicBytes)
        {
        }
    }
}
