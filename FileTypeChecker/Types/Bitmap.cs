namespace FileTypeChecker.Types
{
    using Abstracts;

    public class Bitmap : FileType, IFileType
    {
        public const string TypeName = "Bitmap";
        public const string TypeMimeType = "image/bmp";
        public const string TypeExtension = "bmp";
        private static readonly byte[] MagicBytes = { 0x42, 0x4d };

        public Bitmap() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
