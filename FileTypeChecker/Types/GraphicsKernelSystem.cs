namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class GraphicsKernelSystem : FileType, IFileType
    {
        private const string name = "Graphics Kernel System";
        private const string extension = FileExtension.Gks;
        private static readonly byte[] magicBytes = new byte[] { 0x47, 0x4b, 0x53, 0x4d };

        public GraphicsKernelSystem() : base(name, extension, magicBytes)
        {
        }
    }
}
