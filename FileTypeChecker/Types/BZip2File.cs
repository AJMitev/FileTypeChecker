namespace FileTypeChecker.Types
{
    using Abstracts;

    public class BZip2File : FileType, IFileType
    {
        public const string TypeName = "BZIP2 file";
        public const string TypeMimeType = "application/x-bzip2";
        public const string TypeExtension = "bz2";
        private static readonly byte[] MagicBytes = { 0x42, 0x5A };

        public BZip2File() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
