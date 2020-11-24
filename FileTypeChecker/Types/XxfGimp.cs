namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class XxfGimp : FileType, IFileType
    {
        private const string name = "XCF Gimp file structure";
        private const string extension = FileExtension.Xcf;
        private static readonly byte[] magicBytes = new byte[] { 0x67, 0x69, 0x6d, 0x70, 0x20, 0x78, 0x63, 0x66, 0x20, 0x76 };

        public XxfGimp() : base(name,extension,magicBytes)
        {

        }
    }
}
