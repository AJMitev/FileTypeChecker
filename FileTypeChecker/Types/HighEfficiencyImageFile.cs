namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class HighEfficiencyImageFile : FileType, IFileType
    {
        public const string TypeName = "High Efficiency Image File";
        public const string TypeExtension = "heic";
        private static readonly MagicSequence magicBytes = new(new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x68, 0x65, 0x69, 0x63 });

        public HighEfficiencyImageFile() : base(TypeName, TypeExtension, magicBytes)
        {
        }
    }
}
