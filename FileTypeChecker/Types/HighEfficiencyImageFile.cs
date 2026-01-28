namespace FileTypeChecker.Types
{
    using Abstracts;

    public class HighEfficiencyImageFile : FileType, IFileType
    {
        public const string TypeName = "High Efficiency Image File Format";
        public const string TypeMimeType = "image/heic";
        public const string TypeExtension = "heic";
        private static readonly MagicSequence[] MagicBytes =
        {
            new(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x68, 0x65, 0x69, 0x63 }, 4),
            new(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x6d, 0x69, 0x66, 0x31 }, 4)
        };


        public HighEfficiencyImageFile() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
