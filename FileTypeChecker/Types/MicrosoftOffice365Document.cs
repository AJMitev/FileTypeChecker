namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class MicrosoftOffice365Document : FileType, IFileType
    {
        public const string TypeName = "Microsoft Office 365 Document";
        public const string TypeExtension = "docx";
        private static readonly byte[] MagicBytes = { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 };

        public MicrosoftOffice365Document() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
