namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Wav: FileType, IFileType
    {
        private const string name = "WAV audio file";
        private const string extension = FileExtension.Wav;
        private static readonly byte[] magicBytes = new byte[] { 0x52, 0x49, 0x46, 0x46 };

        public Wav():base(name,extension,magicBytes)
        {

        }
    }
}
