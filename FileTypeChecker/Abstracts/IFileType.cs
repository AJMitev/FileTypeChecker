using System.IO;

namespace FileTypeChecker
{
    public interface IFileType
    {
        string Extension { get; }
        string Name { get; }

        bool Matches(Stream stream);
    }
}