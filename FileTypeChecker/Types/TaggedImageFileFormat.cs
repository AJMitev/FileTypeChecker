namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class TaggedImageFileFormat : FileType, IFileType
    {
        private const string name = "Tagged Image File Format";
        private const string extension = "tif";
        private static readonly byte[][] magicBytesJaggedArray = { new byte[] { 0x49, 0x49, 0x2A, 0x00 }, new byte[] { 0x4D, 0x4D, 0x00, 0x2A } };

        public TaggedImageFileFormat() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
