namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class TarArchive : FileType, IFileType
    {
        private static readonly string name = "TAR Archive";
        private static readonly string extension = "tar";
        private static readonly byte[] magicBytes = new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72 };

        public TarArchive() : base(name, extension, magicBytes)
        {
        }
    }
}
