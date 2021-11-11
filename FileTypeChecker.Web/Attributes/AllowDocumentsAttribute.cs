namespace FileTypeChecker.Web.Attributes
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class AllowDocumentsAttribute : FileTypeValidationWithNoParametersBaseAttribute
    {
        protected override ValidationResult Validate(IFormFile formFile)
        {
            if (!IFormFileTypeValidator.IsTypeRecognizable(formFile))
            {
                return new ValidationResult(this.UnsupportedFileErrorMessage);
            }

            if (!formFile.IsDocument())
            {
                return new ValidationResult(this.ErrorMessage ?? this.InvalidFileTypeErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
