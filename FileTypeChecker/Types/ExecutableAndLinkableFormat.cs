namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class ExecutableAndLinkableFormat : FileType, IFileType
    {
        private static readonly string name = "Executable and Linkable Format";
        private static readonly string extension = "elf";
        private static readonly byte[] magicBytes = new byte[] { 0x7F, 0x45, 0x4C, 0x46 };

        public ExecutableAndLinkableFormat() : base(name, extension, magicBytes)
        {
        }
    }
}
