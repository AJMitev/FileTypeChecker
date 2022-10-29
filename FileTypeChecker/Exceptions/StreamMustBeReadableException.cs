namespace FileTypeChecker.Exceptions
{
    using System;

    public class StreamMustBeReadableException : Exception
    {
        private const string ErrorMessage = "File contents must be a readable stream";

        public StreamMustBeReadableException() : base(ErrorMessage)
        {
        }
    }
}
