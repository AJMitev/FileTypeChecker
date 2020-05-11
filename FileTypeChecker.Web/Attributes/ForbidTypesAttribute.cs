namespace FileTypeChecker.Web.Attributes
{
    using FileTypeChecker.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    public class ForbidTypesAttribute : FileTypeValidationBaseAttribute
    {
        private readonly string[] extensions;

        public ForbidTypesAttribute(params string[] extensions)
            => this.extensions = extensions;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is IFormFile file))
            {
                return ValidationResult.Success;
            }

            if(this.extensions == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NullParameterErrorMessage);
            }

            if(this.extensions.Length == 0)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.InvalidParameterLengthErrorMessage);
            }

            using var stream = new MemoryStream();
            file.CopyTo(stream);

            if (!FileTypeValidator.IsTypeRecognizable(stream))
            {
                return new ValidationResult(this.UnsupportedFileErrorMessage);
            }

            var fileType = FileTypeValidator.GetFileType(stream);

            if (extensions.Contains(fileType.Extension.ToLower()))
            {
                return new ValidationResult(this.ErrorMessage ?? this.InvalidFileTypeErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
