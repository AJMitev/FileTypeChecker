namespace FileTypeChecker.Types
{
    using Abstracts;

    public class Executable : FileType, IFileType
    {
        public const string TypeName = "DOS MZ executable";
        public const string TypeExtension = "exe";
        private static readonly byte[] MagicBytes = { 0x4D, 0x5A };

        public Executable() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
