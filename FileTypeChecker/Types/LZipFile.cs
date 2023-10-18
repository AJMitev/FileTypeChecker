namespace FileTypeChecker.Types
{
    using Abstracts;

    public class LZipFile : FileType, IFileType
    {
        public const string TypeName = "LZIP file";
        public const string TypeExtension = "lz";
        private static readonly MagicSequence MagicBytes = new(new[] { (byte)'L', (byte)'Z', (byte)'I', (byte)'P', (byte)1 });

        public LZipFile() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
