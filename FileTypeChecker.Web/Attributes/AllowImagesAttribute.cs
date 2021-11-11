namespace FileTypeChecker.Web.Attributes
{
    using FileTypeChecker.Extensions;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class AllowImagesAttribute : FileTypeValidationWithNoParametersBaseAttribute
    {
        /// <summary>
        /// Determines whether a specified object is valid. (Overrides <see cref = "ValidationAttribute.IsValid(object)" />)
        /// </summary>
        /// <remarks>
        /// This method returns <c>true</c> if the <paramref name = "value" /> is null.  
        /// It is assumed the <see cref = "RequiredAttribute" /> is used if the value may not be null.
        /// </remarks>
        /// <param name = "value">The object to validate.</param>
        /// <returns><c>true</c> if the value is null or valid, otherwise <c>false</c></returns>
        

        protected override ValidationResult Validate(IFormFile formFile)
        {
            if (!IFormFileTypeValidator.IsTypeRecognizable(formFile))
            {
                return new ValidationResult(this.UnsupportedFileErrorMessage);
            }

            if (!formFile.IsImage())
            {
                return new ValidationResult(this.ErrorMessage ?? this.InvalidFileTypeErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
