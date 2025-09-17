namespace FileTypeChecker.Types
{
    using Abstracts;

    public class AudioVideoInterleaveVideoFormat : FileType, IFileType
    {
        public const string TypeName = "Audio Video Interleave video format";
        public const string TypeExtension = "avi";
        private static readonly MagicSequence[] MagicBytes = { new(new byte[] { 0x52, 0x49, 0x46, 0x46, /*skip-->*/0x00, 0x00, 0x00, 0x00/*<--skip*/, 0x41, 0x56, 0x49, 0x20 }, 4, 4) };

        public AudioVideoInterleaveVideoFormat() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
