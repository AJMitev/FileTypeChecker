using FileTypeChecker.Abstracts;

namespace FileTypeChecker.Types
{
    public class M4V : FileType, IFileType
    {
        public const string TypeName = "M4v file";
        public const string TypeMimeType = "video/mp4";
        public const string TypeExtension = "M4v";

        private static readonly MagicSequence MagicBytesSequence = new(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32 }, 4);

        public M4V() : base(TypeName, TypeMimeType, TypeExtension, MagicBytesSequence)
        {
        }
    }
}