namespace FileTypeChecker.Types
{
    using Abstracts;

    public class OpenDocumentText : FileType, IFileType
    {
        public const string TypeName = "OpenDocument Text";
        public const string TypeMimeType = "application/vnd.oasis.opendocument.text";
        public const string TypeExtension = "odt";

        public OpenDocumentText() : base(TypeName, TypeMimeType, TypeExtension, OpenDocumentSignature.For(TypeMimeType))
        {
        }
    }
}
