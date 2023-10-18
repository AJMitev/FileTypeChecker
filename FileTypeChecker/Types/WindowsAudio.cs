namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class WindowsAudio : FileType, IFileType
    {
        public const string TypeName = "Windows audio file";
        public const string TypeExtension = "wma";
        private static readonly byte[] MagicBytes = { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF };

        public WindowsAudio() : base(TypeName, TypeExtension, MagicBytes) { }
    }
}
