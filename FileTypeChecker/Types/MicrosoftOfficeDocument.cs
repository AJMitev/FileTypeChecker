namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class MicrosoftOfficeDocument : FileType, IFileType
    {
        private static readonly string name = "Microsoft Office Document 97-2003";
        private static readonly string extension = "doc";
        private static readonly byte[] magicBytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };

        public MicrosoftOfficeDocument() : base(name, extension, magicBytes)
        {
        }
    }
}
