namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class ExtensibleMarkupLanguage : FileType, IFileType
    {
        private static readonly string name = "eXtensible Markup Language";
        private static readonly string extension = "xml";
        private static readonly byte[] magicBytes = new byte[] { 0x3c, 0x3f, 0x78, 0x6d, 0x6c, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31 };

        public ExtensibleMarkupLanguage() : base(name, extension, magicBytes)
        {
        }
    }
}
