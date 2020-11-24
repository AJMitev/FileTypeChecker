namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class BZip2File : FileType, IFileType
    {
        private const string name = "BZIP2 file";
        private const string extension = FileExtension.Bz2;
        private static readonly byte[] magicBytes = new byte[] { 0x42, 0x5A };

        public BZip2File() : base(name, extension, magicBytes)
        {
        }
    }
}
