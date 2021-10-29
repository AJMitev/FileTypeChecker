namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class Executable : FileType, IFileType
    {
        public const string TypeName = "DOS MZ executable";
        public const string TypeExtension = "exe";
        private static readonly byte[] magicBytes = new byte[] { 0x4D, 0x5A };

        public Executable() : base(TypeName, TypeExtension, magicBytes)
        {
        }
    }
}
