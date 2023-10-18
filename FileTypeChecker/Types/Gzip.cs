namespace FileTypeChecker.Types
{
    using FileTypeChecker;
    using Abstracts;

    public class Gzip : FileType, IFileType
    {
        public const string TypeName = "GZIP compressed file";
        public const string TypeExtension = "gz";
        private static readonly MagicSequence[] MagicBytesJaggedArray =
        {
            new(new byte[] { 0x1F, 0x8B, 8 }),
            new( new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72 })
        };

        public Gzip() : base(TypeName, TypeExtension, MagicBytesJaggedArray)
        {
        }
    }
}
