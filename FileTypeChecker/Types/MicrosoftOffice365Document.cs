namespace FileTypeChecker.Types
{
    using Abstracts;

    public class MicrosoftOffice365Document : FileType, IFileType
    {
        public const string TypeName = "Microsoft Office 365 Document";
        public const string TypeMimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public const string TypeExtension = "docx";

        private static readonly MagicSequence[] MagicBytes =
        {
            new(new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00, 0x08, 0x00, 0x00, 0x00, 0x21, 0x00 }),
            new(new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x08, 0x08, 0x08, 0x00 }),
            new(new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x00, 0x00, 0x08, 0x00 }),
            new(new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }),
            new(new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC8 })
        };

        public MicrosoftOffice365Document() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}