namespace FileTypeChecker.Tests
{
    using FileTypeChecker.Types;
    using FileTypeChecker.Extensions;
    using NUnit.Framework;
    using System.IO;
    using System.Threading.Tasks;

    [TestFixture]
    public class StreamExtensionsTests
    {
        [Test]
        public void Is_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = true;
            var actual = fileStream.Is<Bitmap>();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Is_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = false;
            var actual = fileStream.Is<Gzip>();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsImage_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.jpg");
            var expected = true;
            var actual = fileStream.IsImage();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsImage_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.exe");
            var expected = false;
            var actual = fileStream.IsImage();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsArchive_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.zip");
            var expected = true;
            var actual = fileStream.IsArchive();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsArchive_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = false;
            var actual = fileStream.IsArchive();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsExecutable_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.exe");
            var expected = true;
            var actual = fileStream.IsExecutable();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsExecutable_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = false;
            var actual = fileStream.IsExecutable();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Document_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = false;
            var actual = fileStream.IsDocument();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsDocument_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.doc");
            var expected = true;
            var actual = fileStream.IsDocument();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task DocumentAsync_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = false;
            var actual = await fileStream.IsDocumentAsync();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task IsDocumentAsync_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.doc");
            var expected = true;
            var actual = await fileStream.IsDocumentAsync();

            Assert.AreEqual(expected, actual);
        }
    }
}
