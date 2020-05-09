namespace FileTypeChecker.Web.Attributes
{
    using FileTypeChecker.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;


    [AttributeUsage(AttributeTargets.Property)]
    public class ForbidTypesAttribute : ValidationAttribute
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

            using var stream = new MemoryStream();
            file.CopyTo(stream);

            if (!FileTypeValidator.IsTypeRecognizable(stream))
            {
                return new ValidationResult(Constants.ErrorMessages.UnsupportedFileErrorMessage);
            }

            var fileType = FileTypeValidator.GetFileType(stream);

            if (extensions.Contains(fileType.Extension.ToLower()))
            {
                return new ValidationResult(Constants.ErrorMessages.InvalidFileTypeErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
