namespace FileTypeChecker.Types
{
    using Abstracts;

    public class ExecutableAndLinkableFormat : FileType, IFileType
    {
        public const string TypeName = "Executable and Linkable Format";
        public const string TypeExtension = "elf";
        private static readonly byte[] MagicBytes = { 0x7F, 0x45, 0x4C, 0x46 };

        public ExecutableAndLinkableFormat() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
