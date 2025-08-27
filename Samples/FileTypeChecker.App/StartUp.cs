namespace FileTypeChecker.App
{
    using Abstracts;
    using MyCustomTypes;
    using Types;
    using Extensions;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class StartUp
    {
        public static async Task Main()
        {
            Console.WriteLine("Choose demo mode:");
            Console.WriteLine("1. Synchronous (original)");
            Console.WriteLine("2. Asynchronous (new)");
            Console.WriteLine("3. Both");
            Console.Write("Enter your choice (1-3): ");
            
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    RunSyncDemo();
                    break;
                case "2":
                    await StartUpAsync.MainAsync();
                    break;
                case "3":
                default:
                    RunSyncDemo();
                    Console.WriteLine("\n" + new string('=', 60));
                    await StartUpAsync.MainAsync();
                    break;
            }
        }

        private static void RunSyncDemo()
        {
            Console.WriteLine("=== FileTypeChecker Synchronous Demo ===");
            Console.WriteLine();

            // You can register your own custom types validation if its needed.
            FileTypeValidator.RegisterCustomTypes(typeof(MyCustomFileType).Assembly);

            for (int i = 1; i <= 14; i++)
            {
                using var fileStream = File.OpenRead(Path.Combine("files", i.ToString()));
                
                Console.WriteLine($"Processing file {i} synchronously...");
                
                var isRecognizableType = FileTypeValidator.IsTypeRecognizable(fileStream);

                if (!isRecognizableType)
                {
                    Console.WriteLine("Unknown file");
                    Console.WriteLine(new string('=', 50));
                    continue;
                }

                IFileType fileType = FileTypeValidator.GetFileType(fileStream);
                Print(fileType, fileStream);
            }

            DemonstrateSyncExtensions();
        }

        private static void Print(IFileType fileType, FileStream fileStream)
        {
            Console.WriteLine("=== Sync File Analysis ===");
            Console.WriteLine($"Is Image?: {fileStream.IsImage()}");
            Console.WriteLine($"Is Archive?: {fileStream.IsArchive()}");
            Console.WriteLine($"Is Document?: {fileStream.IsDocument()}");
            Console.WriteLine($"Is Executable?: {fileStream.IsExecutable()}");
            Console.WriteLine($"Is Bitmap?: {fileStream.Is<Bitmap>()}");
            Console.WriteLine($"Is WebP?: {fileStream.Is<Webp>()}");
            Console.WriteLine($"Type Name: {fileType.Name}");
            Console.WriteLine($"Type Extension: {fileType.Extension}");
            Console.WriteLine(new string('=', 50));
        }

        private static void DemonstrateSyncExtensions()
        {
            Console.WriteLine();
            Console.WriteLine("=== Sync Extensions Demonstration ===");

            var testFiles = new[]
            {
                ("PNG Image", Path.Combine("files", "1")),
                ("PDF Document", Path.Combine("files", "2")), 
                ("ZIP Archive", Path.Combine("files", "3"))
            };

            foreach (var (description, filePath) in testFiles)
            {
                try
                {
                    using var fileStream = File.OpenRead(filePath);
                    
                    Console.WriteLine($"\nAnalyzing {description}:");
                    
                    // Run multiple sync checks
                    var isImage = fileStream.IsImage();
                    var isArchive = fileStream.IsArchive();
                    var isDocument = fileStream.IsDocument();
                    var isExecutable = fileStream.IsExecutable();

                    Console.WriteLine($"  - Is Image: {isImage}");
                    Console.WriteLine($"  - Is Archive: {isArchive}");
                    Console.WriteLine($"  - Is Document: {isDocument}");
                    Console.WriteLine($"  - Is Executable: {isExecutable}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {description}: {ex.Message}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("=== Sync Demo Complete ===");
        }
    }
}
