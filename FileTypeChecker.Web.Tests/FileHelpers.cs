namespace FileTypeChecker.Web.Tests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using System.Collections.Generic;
    using System.IO;

    public static class FileHelpers
    {
        public static IEnumerable<IFormFile> ReadFiles(string[] fileNames)
        {
            var files = new HashSet<IFormFile>();

            foreach (var fileName in fileNames)
            {
                var file = ReadFile(fileName);
                files.Add(file);
            }

            return files;
        }

        public static IFormFile ReadFile(string fileName)
        {
            var fs = new FileStream($"./Files/{fileName}", FileMode.Open, FileAccess.Read, FileShare.Read);
            var file = new FormFile(fs, 0, fs.Length, "Test file", fileName);

            return file;
        }
    }
}
