namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class ExecutableAndLinkableFormat : FileType, IFileType
    {
        private const string name = "Executable and Linkable Format";
        private const string extension = FileExtension.Elf;
        private static readonly byte[] magicBytes = new byte[] { 0x7F, 0x45, 0x4C, 0x46 };

        public ExecutableAndLinkableFormat() : base(name, extension, magicBytes)
        {
        }
    }
}
