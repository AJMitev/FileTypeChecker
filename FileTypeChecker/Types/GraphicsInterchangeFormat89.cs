namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class GraphicsInterchangeFormat89 : FileType, IFileType
    {
        public const string TypeName = "Graphics Interchange Format 89a";
        public const string TypeExtension = "gif";
        private static readonly byte[] MagicBytes = { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 };

        public GraphicsInterchangeFormat89() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
