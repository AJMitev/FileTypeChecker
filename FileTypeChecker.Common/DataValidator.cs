namespace FileTypeChecker.Common
{
    using System;

    public static class DataValidator
    {
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ThrowIfNullOrEmpty(string text, string name)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException($"{name} cannot be null or empty", name);
            }
        }
    }

}