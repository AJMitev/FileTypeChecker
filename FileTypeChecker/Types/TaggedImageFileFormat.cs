namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    internal class TaggedImageFileFormat : FileType, IFileType
    {
        private static readonly string name = "Tagged Image File Format";
        private static readonly string extension = "tif/tiff";
        private static readonly byte[][] magicBytesJaggedArray = { new byte[] { 0x49, 0x49, 0x2A, 0x00 }, new byte[] { 0x4D, 0x4D, 0x00, 0x2A } };

        internal TaggedImageFileFormat() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
