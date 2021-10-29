namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class SevenZipFile : FileType, IFileType
    {
        public const string TypeName = "7-Zip File Format";
        public const string TypeExtension = "7z";
        private static readonly byte[] magicBytes = new byte[] { 0x37, 0x7A, 0xBC, 0xAF, 0x27, 0x1C };

        public SevenZipFile() : base(TypeName, TypeExtension, magicBytes)
        {
        }
    }
}
