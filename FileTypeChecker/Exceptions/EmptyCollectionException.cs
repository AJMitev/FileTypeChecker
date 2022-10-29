namespace FileTypeChecker.Exceptions
{
    using System;

    public class EmptyCollectionException : Exception
    {
        private const string ErrorMessage = "Can't search in collection with no items!";

        public EmptyCollectionException() : base(ErrorMessage)
        {
        }
    }
}
