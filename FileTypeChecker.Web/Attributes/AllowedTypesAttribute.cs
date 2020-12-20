namespace FileTypeChecker.Web.Attributes
{
    using FileTypeChecker.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class AllowedTypesAttribute : FileTypeValidationBaseAttribute
    {
        private readonly string[] extensions;

        public AllowedTypesAttribute(params string[] extensions)
            => this.extensions = extensions;

        /// <summary>
        /// Determines whether a specified object is valid. (Overrides <see cref = "ValidationAttribute.IsValid(object)" />)
        /// </summary>
        /// <remarks>
        /// This method returns <c>true</c> if the <paramref name = "value" /> is null.  
        /// It is assumed the <see cref = "RequiredAttribute" /> is used if the value may not be null.
        /// </remarks>
        /// <param name = "value">The object to validate.</param>
        /// <returns><c>true</c> if the value is null or valid, otherwise <c>false</c></returns>
        /// <exception cref = "InvalidOperationException"></exception>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (this.extensions == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NullParameterErrorMessage);
            }

            if (this.extensions.Length == 0)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.InvalidParameterLengthErrorMessage);
            }

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

        protected override ValidationResult Validate(IFormFile formFile)
        {
            if (!IFormFileTypeValidator.IsTypeRecognizable(formFile))
            {
                return new ValidationResult(this.UnsupportedFileErrorMessage);
            }

            var fileType = IFormFileTypeValidator.GetFileType(formFile);

            if (!extensions.Contains(fileType.Extension.ToLower()))
            {
                return new ValidationResult(this.ErrorMessage ?? this.InvalidFileTypeErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
