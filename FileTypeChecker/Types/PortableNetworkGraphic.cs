namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class PortableNetworkGraphic : FileType, IFileType
    {
        private const string name = "Portable Network Graphic";
        private const string extension = FileExtension.Png;
        private static readonly byte[] magicBytes = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

        public PortableNetworkGraphic() : base(name, extension, magicBytes)
        {
        }
    }
}
