namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class MicrosoftInstaller : FileType, IFileType
    {
        private const string name = "Microsoft Installer";
        private const string extension = FileExtension.Msi;
        private static readonly byte[] magicBytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };

        public MicrosoftInstaller():base(name,extension,magicBytes)
        {

        }
    }
}
