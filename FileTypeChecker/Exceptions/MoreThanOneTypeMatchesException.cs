namespace FileTypeChecker.Exceptions
{
    using System;
    
    public class MoreThanOneTypeMatchesException : Exception
    {
        private const string Message = "More than one type ({0}) matches the provided sequence!";
        private const string MessagePattern = "More than one type ({0}) matches the provided sequence!";

        public MoreThanOneTypeMatchesException() : base(Message)
        {
        }

        public MoreThanOneTypeMatchesException(string types) : base(string.Format(MessagePattern, types))
        {
        }
    }
}
