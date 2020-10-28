namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Bitmap : FileType, IFileType
    {
        private const string name = "Bitmap";
        private const string extension = FileExtension.Bitmap;
        private static readonly byte[] magicBytes = new byte[] { 0x42, 0x4d };

        public Bitmap() : base(name, extension, magicBytes)
        {
        }
    }
}
