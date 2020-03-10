namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Executable : FileType, IFileType
    {
        private static readonly string name = "DOS MZ executable";
        private static readonly string extension = "exe";
        private static readonly byte[] magicBytes = new byte[] { 0x4D, 0x5A };

        public Executable() : base(name, extension, magicBytes)
        {
        }
    }
}
