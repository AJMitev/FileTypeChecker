namespace FileTypeChecker.Web.Infrastructure
{
    /// <summary>
    /// Static class used to contains all "magic" strings
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Static class used to contains all "magic" strings for error messages
        /// </summary>
        internal static class ErrorMessages
        {
            internal const string UnsupportedFileErrorMessage = "Provided file is not of supported type. Please provide a valid file!";
            internal const string InvalidFileTypeErrorMessage = "This type of file is not allowed!";
            internal const string NullParameterErrorMessage = "Provided array of types cannot be null.";
            internal const string InvalidParameterLengthErrorMessage = "Provided array of types must have a Length value that is greater than zero.";
        }
    }
}
