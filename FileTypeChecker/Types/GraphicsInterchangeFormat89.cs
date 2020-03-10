namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class GraphicsInterchangeFormat89 : FileType, IFileType
    {
        private static readonly string name = "Graphics Interchange Format 89a";
        private static readonly string extension = "gif";
        private static readonly byte[] magicBytes = new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 };

        public GraphicsInterchangeFormat89() : base(name, extension, magicBytes)
        {
        }
    }
}
