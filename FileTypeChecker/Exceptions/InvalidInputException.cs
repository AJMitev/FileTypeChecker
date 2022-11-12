namespace FileTypeChecker.Exceptions
{
    using System;

    public class InvalidInputException : Exception
    {
        private const string ErrorMessage = "The provided input is invalid! {0}";

        public InvalidInputException(string message) : base(string.Format(ErrorMessage,message))
        {
        }
    }
}
