namespace FileTypeChecker.Types
{
    using Abstracts;

    public class WaveformAudioFileFormat : FileType, IFileType
    {
        public const string TypeName = "WAV audio file";
        public const string TypeMimeType = "audio/wav";
        public const string TypeExtension = "wav";
        private static readonly MagicSequence[] MagicBytes = { new(new byte[] { 0x52, 0x49, 0x46, 0x46, /*skip-->*/0x00, 0x00, 0x00, 0x00/*<--skip*/, 0x57, 0x41, 0x56, 0x45 }, 4, 4) };

        public WaveformAudioFileFormat() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}