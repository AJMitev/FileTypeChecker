namespace FileTypeChecker.App
{
    using System;
    using System.IO;
    using Abstracts;

    public class StartUp
    {
        public static void Main()
        {
            for (int i = 1; i <= 12; i++)
            {
                using var fileStream = File.OpenRead($".\\files\\{i}");
                var isRecognizableType = FileTypeValidator.IsTypeRecognizable(fileStream);

                if (!isRecognizableType)
                {
                    Console.WriteLine("Unknown file");
                    Console.WriteLine(new string('=', 10));
                    continue;
                }

                IFileType fileType = FileTypeValidator.GetFileType(fileStream);
                Console.WriteLine("Type Name: {0}", fileType.Name);
                Console.WriteLine("Type Extension: {0}", fileType.Extension);
                Console.WriteLine(new string('=', 10));
            }
        }
    }
}
