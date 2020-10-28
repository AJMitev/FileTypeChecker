namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class GraphicsInterchangeFormat87 : FileType, IFileType
    {
        private const string name = "Graphics Interchange Format 87a";
        private const string extension = FileExtension.Gif;
        private static readonly byte[] magicBytes = new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 };

        public GraphicsInterchangeFormat87() : base(name, extension, magicBytes)
        {
        }
    }
}
