namespace FileTypeChecker.Web.Tests
{
    using FileTypeChecker;
    using FileTypeChecker.Web.Attributes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using NUnit.Framework;
    using System.IO;

    [TestFixture]
    public class AttributesTest
    {
        [Test]
        [TestCase("test.bmp", new[] { FileExtension.Bitmap }, true)]
        [TestCase("test.bmp", new[] { FileExtension.Jpg }, false)]
        [TestCase("test.jpg", new[] { FileExtension.Jpg }, true)]
        [TestCase("test.jpg", new[] { FileExtension.Bitmap, FileExtension.Jpg, FileExtension.Gif }, true)]
        [TestCase("test.jpg", new[] { FileExtension.Rar }, false)]
        [TestCase("test.zip", new[] { FileExtension.Zip }, true)]
        public void AllowedTypeAttributeShouldAllowOnlyPointedTypes(string fileName, string[] allowedExtensions, bool expectedResult)
        {
            var stream = ReadFile(fileName);

            var attributeToTest = new AllowedTypesAttribute(allowedExtensions);

            Assert.AreEqual(expectedResult, attributeToTest.IsValid(stream));
        }

        [Test]
        [TestCase("test.zip", new[] { FileExtension.Zip }, false)]
        [TestCase("test.png", new[] { FileExtension.Zip }, true)]
        [TestCase("test.png", new[] { FileExtension.Zip, FileExtension.Xar, FileExtension.Jpg }, true)]
        [TestCase("test.png", new[] { FileExtension.Zip, FileExtension.Png, FileExtension.Jpg }, false)]
        public void ForbidTypesAttributeShouldForbidPointedTypes(string fileName, string[] forbidedExtensions, bool expectedResult)
        {
            var stream = ReadFile(fileName);

            var attributeToTest = new ForbidTypesAttribute(forbidedExtensions);

            Assert.AreEqual(expectedResult, attributeToTest.IsValid(stream));
        }

        [Test]
        [TestCase("test.png", false)]
        [TestCase("test.jpg", false)]
        [TestCase("test.bmp", false)]
        [TestCase("test.zip", true)]
        [TestCase("test.7z", true)]
        [TestCase("test.bz2", true)]
        [TestCase("test.gz", true)]
        public void OnlyArchiveAttributeShouldReturnValidateIfTheFileIsArchive(string fileName, bool expectedResult)
        {
            var stream = ReadFile(fileName);

            var attributeToTest = new OnlyArchiveAttribute();

            Assert.AreEqual(expectedResult, attributeToTest.IsValid(stream));
        }

        [Test]
        [TestCase("test.png", true)]
        [TestCase("test.jpg", true)]
        [TestCase("test.bmp", true)]
        [TestCase("test.zip", false)]
        public void OnlyImageAttributeShouldValidateIfTheFileIsImage(string fileName, bool expectedResult)
        {
            var stream = ReadFile(fileName);

            var attributeToTest = new OnlyImageAttribute();

            Assert.AreEqual(expectedResult, attributeToTest.IsValid(stream));
        }

        [Test]
        [TestCase("test.exe")]
        public void ForbidExecutableFilesAttributeShouldReturnInvalidIfTheFileIsExecutable(string filename)
        {
            var stream = ReadFile(filename);

            var attributeToTest = new ForbidExecutableFileAttribute();

            Assert.IsFalse(attributeToTest.IsValid(stream));
        }

        private IFormFile ReadFile(string fileName)
        {
            var fs = new FileStream($"./Files/{fileName}", FileMode.Open, FileAccess.Read, FileShare.Read);
            var file = new FormFile(fs, 0, fs.Length, "Test file", fileName);

            return file;
        }
    }
}
