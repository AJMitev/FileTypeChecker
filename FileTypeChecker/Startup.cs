namespace FileTypeChecker
{
    using System;
    using System.IO;

    public class Startup
    {
        public static void Main()
        {
            var checker = new FileTypeChecker();
            using (var fileStream = File.OpenRead(@".\sample.xml"))
            {
                var result = checker.GetFileType(fileStream);
                Console.WriteLine("FileType: {0}", result.Name);
            }
        }
    }
}
