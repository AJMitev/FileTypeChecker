namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Bitmap : FileType, IFileType
    {
        private static readonly string name = "Bitmap";
        private static readonly string extension = "bmp";
        private static readonly byte[] magicBytes = new byte[] { 0x42, 0x4d };

        public Bitmap() : base(name, extension, magicBytes)
        {
        }
    }
}
