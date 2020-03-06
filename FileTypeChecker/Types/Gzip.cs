namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class Gzip : FileType, IFileType
    {
        private static readonly string name = "GZIP compressed file";
        private static readonly string extension = "gz";
        private static readonly byte[] magicBytes = new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72 };

        internal Gzip() : base(name, extension, magicBytes)
        {
        }
    }
}
