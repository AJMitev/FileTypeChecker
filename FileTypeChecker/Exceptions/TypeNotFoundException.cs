namespace FileTypeChecker.Exceptions
{
    using System;

    public class TypeNotFoundException : Exception
    {
        private const string ErrorMessage = "No registered type matches the provided sequence!";

        public TypeNotFoundException() : base(ErrorMessage)
        {
        }

        public TypeNotFoundException(Exception innerException) : base(ErrorMessage, innerException)
        {
        }
    }
}
