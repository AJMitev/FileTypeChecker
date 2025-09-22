using FileTypeChecker.Extensions;
using FileTypeChecker.Types;

namespace FileTypeChecker.Tests
{
    using System.IO;
    using System.Threading.Tasks;
    using Types;
    using NUnit.Framework;

    [TestFixture]
    public class StreamExtensionsAsyncTests
    {
        private const string FilesPath = "./files/";

        [Test]
        public async Task IsAsync_ShouldReturnTrueIfTheTypesMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var result = await fileStream.IsAsync<PortableNetworkGraphic>();

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsAsync_ShouldReturnFalseIfTypesDidNotMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var result = await fileStream.IsAsync<Bitmap>();

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsAsync_ShouldReturnFalseIfRandomInput()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test"));
            var result = await fileStream.IsAsync<Bitmap>();

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsImageAsync_ShouldReturnTrueIfTheTypesMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var result = await fileStream.IsImageAsync();

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsImageAsync_ShouldReturnFalseIfTypesDidNotMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.pdf"));
            var result = await fileStream.IsImageAsync();

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsArchiveAsync_ShouldReturnTrueIfTheTypesMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.zip"));
            var result = await fileStream.IsArchiveAsync();

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsArchiveAsync_ShouldReturnFalseIfTypesDidNotMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var result = await fileStream.IsArchiveAsync();

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsExecutableAsync_ShouldReturnTrueIfTheTypesMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.exe"));
            var result = await fileStream.IsExecutableAsync();

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsExecutableAsync_ShouldReturnFalseIfTypesDidNotMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var result = await fileStream.IsExecutableAsync();

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsDocumentAsync_ShouldReturnTrueIfTheTypesMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.pdf"));
            var result = await fileStream.IsDocumentAsync();

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsDocumentAsync_ShouldReturnFalseIfTypesDidNotMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var result = await fileStream.IsDocumentAsync();

            Assert.IsFalse(result);
        }

        [Test]
        public async Task AsyncExtensions_ShouldWorkWithMultipleFileTypes()
        {
            var testFiles = new[]
            {
                ("test.png", true, false, false, false),   // Image
                ("test.zip", false, true, false, false),   // Archive
                ("test.pdf", false, false, false, true),   // Document
                ("test.exe", false, false, true, false),   // Executable
            };

            foreach (var (fileName, isImage, isArchive, isExecutable, isDocument) in testFiles)
            {
                await using var fileStream = File.OpenRead(Path.Combine(FilesPath, fileName));
                
                var actualIsImage = await fileStream.IsImageAsync();
                var actualIsArchive = await fileStream.IsArchiveAsync();
                var actualIsExecutable = await fileStream.IsExecutableAsync();
                var actualIsDocument = await fileStream.IsDocumentAsync();

                Assert.AreEqual(isImage, actualIsImage, $"{fileName} image check failed");
                Assert.AreEqual(isArchive, actualIsArchive, $"{fileName} archive check failed");
                Assert.AreEqual(isExecutable, actualIsExecutable, $"{fileName} executable check failed");
                Assert.AreEqual(isDocument, actualIsDocument, $"{fileName} document check failed");
            }
        }
    }
}