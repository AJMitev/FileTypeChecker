namespace FileTypeChecker.Web.Attributes
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class FileTypeValidationWithNoParametersBaseAttribute : FileTypeValidationBaseAttribute
    {
        /// <summary>
        /// Determines whether a specified object is valid. (Overrides <see cref = "ValidationAttribute.IsValid(object)" />)
        /// </summary>
        /// <remarks>
        /// This method returns <c>true</c> if the <paramref name = "value" /> is null.  
        /// It is assumed the <see cref = "RequiredAttribute" /> is used if the value may not be null.
        /// </remarks>
        /// <param name = "value">The object to validate.</param>
        /// <returns><c>true</c> if the value is null or greater than or valid, otherwise <c>false</c></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                return this.Validate(file);
            }

            if (value is IEnumerable<IFormFile> files)
            {
                foreach (var formFile in files)
                {
                    var validationResult = this.Validate(formFile);
                    if (validationResult != ValidationResult.Success)
                    {
                        return validationResult;
                    }
                }
            }

            return ValidationResult.Success;
        }

        protected abstract override ValidationResult Validate(IFormFile formFile);
    }
}
