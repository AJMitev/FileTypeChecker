namespace FileTypeChecker.Tests
{
    using Types;
    using Extensions;
    using NUnit.Framework;
    using System.IO;

    [TestFixture]
    public class StreamExtensionsTests
    {
        [Test]
        public void Is_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var actual = fileStream.Is<Bitmap>();

            Assert.IsTrue(actual);
        }

        [Test]
        public void Is_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var actual = fileStream.Is<Gzip>();

            Assert.IsFalse(actual);
        }

        [Test]
        public void IsImage_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.jpg");
            var actual = fileStream.IsImage();

            Assert.IsTrue( actual);
        }

        [Test]
        public void IsImage_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.exe");
            var actual = fileStream.IsImage();

            Assert.IsFalse(actual);
        }

        [Test]
        public void IsArchive_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.zip");
            var actual = fileStream.IsArchive();

            Assert.IsTrue(actual);
        }

        [Test]
        public void IsArchive_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var actual = fileStream.IsArchive();

            Assert.IsFalse(actual);
        }

        [Test]
        public void IsExecutable_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.exe");
            var actual = fileStream.IsExecutable();

            Assert.IsTrue(actual);
        }

        [Test]
        public void IsExecutable_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var actual = fileStream.IsExecutable();

            Assert.IsFalse(actual);
        }

        [Test]
        public void Document_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var actual = fileStream.IsDocument();

            Assert.IsFalse(actual);
        }

        [Test]
        public void IsDocument_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.doc");
            var actual = fileStream.IsDocument();

            Assert.IsTrue(actual);
        }

        [Test]
        public void Is_ShouldReturnFalseIfRandomInput()
        {
            using var memoryStream = new MemoryStream(new byte[] {4, 6, 210, 16, 48, 31, 48, 45});
            var actual = memoryStream.Is<Gzip>();

            Assert.IsFalse(actual);
        }
    }
}
