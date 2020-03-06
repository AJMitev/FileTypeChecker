namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class PortableNetworkGraphic : FileType, IFileType
    {
        private static readonly string name = "Portable Network Graphic";
        private static readonly string extension = "png";
        private static readonly byte[] magicBytes = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

        internal PortableNetworkGraphic() : base(name, extension, magicBytes)
        {
        }
    }
}
