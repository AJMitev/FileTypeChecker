namespace FileTypeChecker.App
{
    using FileTypeChecker.Abstracts;
    using FileTypeChecker.App.MyCustomTypes;
    using FileTypeChecker.Types;
    using FileTypeChecker.Extensions;
    using System;
    using System.IO;

    public static class StartUp
    {
        public static void Main()
        {
            // You can register your own custom types validation if its needed.
            FileTypeValidator.RegisterCustomTypes(typeof(MyCustomFileType).Assembly);

            for (int i = 1; i <= 14; i++)
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
                Print(fileType, fileStream);

                // Or you can use the safe method
                var result = FileTypeValidator.TryGetFileType(fileStream);
                if (!result.HasMatch)
                {
                    Console.WriteLine("Unknown file");
                    Console.WriteLine(new string('=', 10));
                    continue;
                }

                Print(result.Type, fileStream);
            }
        }

        private static void Print(IFileType fileType, FileStream fileStream)
        {
            Console.WriteLine("Is Image?: {0}", fileStream.IsImage());
            Console.WriteLine("Is Bitmap?: {0}", fileStream.Is<Bitmap>());
            Console.WriteLine("Is WebP?: {0}", fileStream.Is<Webp>());
            Console.WriteLine("Type Name: {0}", fileType.Name);
            Console.WriteLine("Type Extension: {0}", fileType.Extension);
            Console.WriteLine(new string('=', 10));
        }
    }
}
