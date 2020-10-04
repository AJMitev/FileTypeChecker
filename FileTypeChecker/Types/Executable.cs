namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Executable : FileType, IFileType
    {
        private const string name = "DOS MZ executable";
        private const string extension = "exe";
        private static readonly byte[] magicBytes = new byte[] { 0x4D, 0x5A };

        public Executable() : base(name, extension, magicBytes)
        {
        }
    }
}
