namespace FileTypeChecker.Web.Attributes
{
    using FileTypeChecker.Extensions;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class ForbidExecutablesAttribute : FileTypeValidationWithNoParametersBaseAttribute
    {
        protected override ValidationResult Validate(IFormFile formFile)
        {
            if (!IFormFileTypeValidator.IsTypeRecognizable(formFile))
            {
                return new ValidationResult(this.UnsupportedFileErrorMessage);
            }

            if (formFile.IsExecutable())
            {
                return new ValidationResult(this.ErrorMessage ?? this.InvalidFileTypeErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}