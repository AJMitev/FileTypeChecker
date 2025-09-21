namespace FileTypeChecker.App
{
    using Abstracts;
    using MyCustomTypes;
    using Types;
    using Extensions;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class StartUpAsync
    {
        public static async Task MainAsync()
        {
            Console.WriteLine("=== FileTypeChecker Async Demo ===");
            Console.WriteLine();

            // You can register your own custom types validation if its needed.
            FileTypeValidator.RegisterCustomTypes(typeof(MyCustomFileType).Assembly);

            for (int i = 1; i <= 14; i++)
            {
                using var fileStream = File.OpenRead(Path.Combine("files", i.ToString()));
                
                Console.WriteLine($"Processing file {i} asynchronously...");
                
                var isRecognizableType = await FileTypeValidator.IsTypeRecognizableAsync(fileStream);

                if (!isRecognizableType)
                {
                    Console.WriteLine("Unknown file");
                    Console.WriteLine(new string('=', 50));
                    continue;
                }

                IFileType fileType = await FileTypeValidator.GetFileTypeAsync(fileStream);
                await PrintAsync(fileType, fileStream);
            }

            await DemonstrateAsyncExtensionsAsync();
        }

        private static async Task PrintAsync(IFileType fileType, FileStream fileStream)
        {
            Console.WriteLine("=== Async File Analysis ===");
            Console.WriteLine($"Is Image?: {await fileStream.IsImageAsync()}");
            Console.WriteLine($"Is Archive?: {await fileStream.IsArchiveAsync()}");
            Console.WriteLine($"Is Document?: {await fileStream.IsDocumentAsync()}");
            Console.WriteLine($"Is Executable?: {await fileStream.IsExecutableAsync()}");
            Console.WriteLine($"Is Bitmap?: {await fileStream.IsAsync<Bitmap>()}");
            Console.WriteLine($"Is WebP?: {await fileStream.IsAsync<Webp>()}");
            Console.WriteLine($"Type Name: {fileType.Name}");
            Console.WriteLine($"Mime Type: {fileType.MimeType}");
            Console.WriteLine($"Type Extension: {fileType.Extension}");
            Console.WriteLine(new string('=', 50));
        }

        private static async Task DemonstrateAsyncExtensionsAsync()
        {
            Console.WriteLine();
            Console.WriteLine("=== Async Extensions Demonstration ===");

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
                    
                    // Run multiple async checks concurrently
                    var imageTask = fileStream.IsImageAsync();
                    var archiveTask = fileStream.IsArchiveAsync();
                    var documentTask = fileStream.IsDocumentAsync();
                    var executableTask = fileStream.IsExecutableAsync();

                    // Wait for all async operations to complete
                    await Task.WhenAll(imageTask, archiveTask, documentTask, executableTask);

                    Console.WriteLine($"  - Is Image: {imageTask.Result}");
                    Console.WriteLine($"  - Is Archive: {archiveTask.Result}");
                    Console.WriteLine($"  - Is Document: {documentTask.Result}");
                    Console.WriteLine($"  - Is Executable: {executableTask.Result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {description}: {ex.Message}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("=== Async Demo Complete ===");
        }
    }
}