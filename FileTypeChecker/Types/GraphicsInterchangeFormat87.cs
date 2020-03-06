namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class GraphicsInterchangeFormat87 : FileType, IFileType
    {
        private static readonly string name = "Graphics Interchange Format 87a";
        private static readonly string extension = "gif";
        private static readonly byte[] magicBytes = new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 };

        public GraphicsInterchangeFormat87() : base(name, extension, magicBytes)
        {
        }
    }
}
