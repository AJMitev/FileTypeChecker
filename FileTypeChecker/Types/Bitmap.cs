namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Bitmap : FileType, IFileType
    {
        public const string TypeName = "Bitmap";
        public const string TypeExtension = "bmp";
        private static readonly byte[] magicBytes = new byte[] { 0x42, 0x4d };

        public Bitmap() : base(TypeName, TypeExtension, magicBytes)
        {
        }
    }
}
