namespace FileTypeChecker.Exceptions
{
    using System;
    
    public class MoreThanOneTypeMatchesException : Exception
    {
        private const string ErrorMessage = "More than one type matches the provided sequence!";
        private const string ErrorMessagePattern = "More than one type ({0}) matches the provided sequence!";

        public MoreThanOneTypeMatchesException() : base(ErrorMessage)
        {
        }

        public MoreThanOneTypeMatchesException(string types) : base(string.Format(ErrorMessagePattern, types))
        {
        }
    }
}
