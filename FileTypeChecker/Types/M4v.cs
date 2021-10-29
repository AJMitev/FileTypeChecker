using FileTypeChecker.Abstracts;

namespace FileTypeChecker.Types
{
    public class M4v : FileType, IFileType
    {
        public const string TypeName = "M4v file";
        public const string TypeExtension = "M4v";

        private static readonly byte[][] magicBytesJaggedArray =
            {new byte[] {0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32}};

        public M4v() : base(TypeName, TypeExtension, magicBytesJaggedArray, 4)
        {
        }
    }
}