namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Icon : FileType, IFileType
    {
        private const string name = "Icon";
        private const string extension = FileExtension.Ico;
        private static readonly byte[] magicBytes = new byte[] { 0x00, 0x00, 0x01, 0x00};

        public Icon():base(name,extension,magicBytes)
        {

        }
    }
}
