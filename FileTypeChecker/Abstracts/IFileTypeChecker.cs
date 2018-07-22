using System.Collections.Generic;
using System.IO;

namespace FileTypeChecker
{
    public interface IFileTypeChecker
    {
        IFileType GetFileType(Stream fileContent);
        IEnumerable<IFileType> GetFileTypes(Stream stream);
    }
}