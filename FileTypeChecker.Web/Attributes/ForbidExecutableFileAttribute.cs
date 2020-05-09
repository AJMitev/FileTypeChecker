namespace FileTypeChecker.Web.Attributes
{
    using FileTypeChecker.Extensions;
    using FileTypeChecker.Types;
    using FileTypeChecker.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    [AttributeUsage(AttributeTargets.Property)]
    public class ForbidExecutableFileAttribute : ValidationAttribute
    {
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

            if (stream.Is<Executable>() || stream.Is<ExecutableAndLinkableFormat>())
            {
                return new ValidationResult(Constants.ErrorMessages.InvalidFileTypeErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}