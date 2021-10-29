namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class MicrosoftOfficeDocument : FileType, IFileType
    {
        public const string TypeName = "Microsoft Office Document 97-2003";
        public const string TypeExtension = "doc";
        private static readonly byte[] magicBytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };

        public MicrosoftOfficeDocument() : base(TypeName, TypeExtension, magicBytes)
        {
        }
    }
}
