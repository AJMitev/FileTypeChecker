namespace FileTypeChecker.Extensions
{
    using System;
    using Abstracts;

    public static class IFileTypeExtensions
    {
        public static bool MatchesExtension(this IFileType filetype, string extension)
        {
            return filetype.Extension.Equals(extension, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
