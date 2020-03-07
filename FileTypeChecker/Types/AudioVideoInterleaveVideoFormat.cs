namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class AudioVideoInterleaveVideoFormat : FileType, IFileType
    {
        private static readonly string name = "Audio Video Interleave video format";
        private static readonly string extension = "avi";
        private static readonly byte[] magicBytes = new byte[] { 0x52, 0x49, 0x46, 0x46 };

        public AudioVideoInterleaveVideoFormat() : base(name, extension, magicBytes)
        {
        }
    }
}
