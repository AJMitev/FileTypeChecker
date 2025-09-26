namespace FileTypeChecker.Types
{
    using FileTypeChecker;
    using Abstracts;

    public class RarArchive : FileType, IFileType
    {
        public const string TypeName = "RAR archive";
        public const string TypeMimeType = "application/vnd.rar";
        public const string TypeExtension = "rar";
        private static readonly MagicSequence[] MagicBytes =
        { 
           new(new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00 }),
           new(new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00 })
        };

        public RarArchive() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
