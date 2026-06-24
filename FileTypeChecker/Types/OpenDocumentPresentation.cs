namespace FileTypeChecker.Types
{
    using Abstracts;

    public class OpenDocumentPresentation : FileType, IFileType
    {
        public const string TypeName = "OpenDocument Presentation";
        public const string TypeMimeType = "application/vnd.oasis.opendocument.presentation";
        public const string TypeExtension = "odp";

        public OpenDocumentPresentation() : base(TypeName, TypeMimeType, TypeExtension, OpenDocumentSignature.For(TypeMimeType))
        {
        }
    }
}
