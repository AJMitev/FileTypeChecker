using System.IO;

namespace FileTypeChecker
{
    public interface IFileTypeMatcher
    {
        bool Matches(Stream stream, bool resetPosition = true);
    }
}