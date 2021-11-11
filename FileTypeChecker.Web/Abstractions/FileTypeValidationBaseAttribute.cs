namespace FileTypeChecker.Web.Attributes
{
    using FileTypeChecker.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public abstract class FileTypeValidationBaseAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets error message for unsupported types of file.
        /// </summary>
        public string UnsupportedFileErrorMessage { get; set; } = Constants.ErrorMessages.UnsupportedFileErrorMessage;

        /// <summary>
        /// Gets or sets error message for invalid types of file.
        /// </summary>
        public string InvalidFileTypeErrorMessage { get; set; } = Constants.ErrorMessages.InvalidFileTypeErrorMessage;

        /// <summary>
        /// Determines whether a specified object is valid. (Overrides <see cref = "ValidationAttribute.IsValid(object)" />)
        /// </summary>
        /// <remarks>
        /// This method returns <c>true</c> if the <paramref name = "value" /> is null.  
        /// It is assumed the <see cref = "RequiredAttribute" /> is used if the value may not be null.
        /// </remarks>
        /// <param name = "value">The object to validate.</param>
        /// <returns><c>true</c> if the value is null or greater than or valid, otherwise <c>false</c></returns>
        protected abstract override ValidationResult IsValid(object value, ValidationContext validationContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        protected abstract ValidationResult Validate(IFormFile formFile);
    }
}
