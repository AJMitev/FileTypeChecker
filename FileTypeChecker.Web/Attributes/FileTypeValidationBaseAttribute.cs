namespace FileTypeChecker.Web.Attributes
{
    using FileTypeChecker.Web.Infrastructure;
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public abstract class FileTypeValidationBaseAttribute : ValidationAttribute
    {
        public string UnsupportedFileErrorMessage { get; set; } = Constants.ErrorMessages.UnsupportedFileErrorMessage;
        public string InvalidFileTypeErrorMessage { get; set; } = Constants.ErrorMessages.InvalidFileTypeErrorMessage;

        protected abstract override ValidationResult IsValid(object value, ValidationContext validationContext);
    }
}
