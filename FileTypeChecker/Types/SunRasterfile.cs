namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class SunRasterfile : FileType, IFileType
    {
        private const string name = "Sun Rasterfile";
        private const string extension = FileExtension.Ras;
        private static readonly byte[] magicBytes = new byte[] { 0x59, 0xa6, 0x6a, 0x95 };

        public SunRasterfile() : base(name, extension, magicBytes)
        { }
    }
}
