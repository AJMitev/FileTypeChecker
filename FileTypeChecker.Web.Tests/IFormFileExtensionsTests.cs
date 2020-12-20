namespace FileTypeChecker.Web.Tests
{
    using FileTypeChecker.Types;
    using NUnit.Framework;

    [TestFixture]
    public class IFormFileExtensionsTests
    {
        [Test]
        public void Is_ShouldReturnTrueIfTheTypesMatch()
        {
            var formFile = FileHelpers.ReadFile("test.bmp");
            var expected = true;
            var actual = formFile.Is<Bitmap>();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Is_ShouldReturnFalseIfTypesDidNotMatch()
        {
            var formFile = FileHelpers.ReadFile("test.bmp");
            var expected = false;
            var actual = formFile.Is<Gzip>();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsImage_ShouldReturnTrueIfTheTypesMatch()
        {
            var formFile = FileHelpers.ReadFile("test.jpg");
            var expected = true;
            var actual = formFile.IsImage();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsImage_ShouldReturnFalseIfTypesDidNotMatch()
        {
            var formFile = FileHelpers.ReadFile("test.exe");
            var expected = false;
            var actual = formFile.IsImage();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsArchive_ShouldReturnTrueIfTheTypesMatch()
        {
            var formFile = FileHelpers.ReadFile("test.zip");
            var expected = true;
            var actual = formFile.IsArchive();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsArchive_ShouldReturnFalseIfTypesDidNotMatch()
        {
            var formFile = FileHelpers.ReadFile("test.bmp");
            var expected = false;
            var actual = formFile.IsArchive();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsExecutable_ShouldReturnTrueIfTheTypesMatch()
        {
            var formFile = FileHelpers.ReadFile("test.exe");
            var expected = true;
            var actual = formFile.IsExecutable();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsExecutable_ShouldReturnFalseIfTypesDidNotMatch()
        {
            var formFile = FileHelpers.ReadFile("test.bmp");
            var expected = false;
            var actual = formFile.IsExecutable();

            Assert.AreEqual(expected, actual);
        }
    }
}
