namespace FileTypeChecker.Exceptions
{
    using System;

    public class TypeNotFoundException : Exception
    {
        private const string Message = "No registered type matches the provided sequence!";

        public TypeNotFoundException() : base(Message)
        {
        }

        public TypeNotFoundException(Exception innerException) : base(Message, innerException)
        {
        }
    }
}
