namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class WindowsMetaFileType : FileType, IFileType 
    {
        public const string TypeName = "Windows Meta File";
        public const string TypeExtension = "wmf";
        private static readonly byte[] magicBytes = new byte[] { 0xD7, 0xCD, 0xC6, 0x9A };

        public WindowsMetaFileType() : base(TypeName, TypeExtension, magicBytes)
        { }
    }
}
