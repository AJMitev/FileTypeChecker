namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class WindowsAudio : FileType, IFileType
    {
        private const string name = "Windows audio file";
        private const string extension = FileExtension.Wma;
        private static readonly byte[] magicBytes = new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF };

        public WindowsAudio() : base(name, extension, magicBytes)
        {

        }
    }
}
