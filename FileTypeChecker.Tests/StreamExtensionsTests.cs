namespace FileTypeChecker.Tests
{
    using FileTypeChecker.Types;
    using FileTypeChecker.Extensions;
    using NUnit.Framework;
    using System.IO;

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
    }
}
