namespace FileTypeChecker.Types
{
    using Abstracts;

    public class GraphicsInterchangeFormat87 : FileType, IFileType
    {
        public const string TypeName = "Graphics Interchange Format 87a";
        public const string TypeMimeType = "image/gif";
        public const string TypeExtension = "gif";
        private static readonly byte[] MagicBytes = { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 };

        public GraphicsInterchangeFormat87() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
