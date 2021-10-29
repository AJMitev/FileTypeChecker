using FileTypeChecker.Abstracts;
using FileTypeChecker.App.MyCustomTypes;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileTypeChecker.AsyncApp
{
    class Startup
    {
        static async Task Main()
        {
            // You can register your own custom types validation if its needed.
            FileTypeValidator.RegisterCustomTypes(typeof(MyCustomFileType).Assembly);

            for (int i = 1; i <= 12; i++)
            {
                using var fileStream = File.OpenRead($".\\files\\{i}");
                var isRecognizableType = await FileTypeValidator.IsTypeRecognizableAsync(fileStream);

                if (!isRecognizableType)
                {
                    Console.WriteLine("Unknown file");
                    Console.WriteLine(new string('=', 10));
                    continue;
                }

                IFileType fileType = await FileTypeValidator.GetFileTypeAsync(fileStream);

                Console.WriteLine("Is Image?: {0}", await fileStream.IsImageAsync());
                Console.WriteLine("Is Bitmap?: {0}", await fileStream.IsAsync<Bitmap>());
                Console.WriteLine("Type Name: {0}", fileType.Name);
                Console.WriteLine("Type Extension: {0}", fileType.Extension);
                Console.WriteLine(new string('=', 10));
            }
        }
    }
}
