namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class AdobeIllustrator : FileType, IFileType
    {
        private const string name = "Adobe Illustrator";
        private const string extension = FileExtension.Ai;
        private static readonly byte[] magicBytes = new byte[] { 0x25, 0x50, 0x44, 0x46 };

        public AdobeIllustrator() : base(name,extension,magicBytes)
        {

        }
    }
}
