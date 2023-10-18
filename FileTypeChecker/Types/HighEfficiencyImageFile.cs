namespace FileTypeChecker.Types
{
    using Abstracts;

    public class HighEfficiencyImageFile : FileType, IFileType
    {
        public const string TypeName = "High Efficiency Image File Format";
        public const string TypeExtension = "heic";
        private static readonly MagicSequence[] MagicBytes =
        {
            new(new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x68, 0x65, 0x69, 0x63 }),
            new(new byte[]{ 0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70, 0x6d, 0x69, 0x66, 0x31 })
        };


        public HighEfficiencyImageFile() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
