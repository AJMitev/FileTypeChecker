namespace FileTypeChecker.Types
{
    using Abstracts;

    public class OpenDocumentSpreadsheet : FileType, IFileType
    {
        public const string TypeName = "OpenDocument Spreadsheet";
        public const string TypeMimeType = "application/vnd.oasis.opendocument.spreadsheet";
        public const string TypeExtension = "ods";

        public OpenDocumentSpreadsheet() : base(TypeName, TypeMimeType, TypeExtension, OpenDocumentSignature.For(TypeMimeType))
        {
        }
    }
}
