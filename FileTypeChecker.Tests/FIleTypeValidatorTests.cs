namespace FileTypeChecker.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using FileTypeChecker.Types;
    using NUnit.Framework;

    [TestFixture]
    public class FileTypeValidatorTests
    {
        [Test]
        public void IsTypeRecognizable_ShouldThrowArgumentNullExceptionIfStreamIsNull()
            => Assert.Catch<ArgumentNullException>(() => FileTypeValidator.IsTypeRecognizable(null));

        [Test]
        public void IsTypeRecognizableAsync_ShouldThrowArgumentNullExceptionIfStreamIsNull()
            => Assert.ThrowsAsync<ArgumentNullException>(async () => await FileTypeValidator.IsTypeRecognizableAsync(null));

        [Test]
        public void IsTypeRecognizable_ShouldReturnFalseIfFormatIsUnknown()
        {
            using var fileStream = File.OpenRead("./files/test");

            var expected = false;

            var actual = FileTypeValidator.IsTypeRecognizable(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task IsTypeRecognizableAsync_ShouldReturnFalseIfFormatIsUnknown()
        {
            using var fileStream = File.OpenRead("./files/test");

            var expected = false;

            var actual = await FileTypeValidator.IsTypeRecognizableAsync(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("./files/test.bmp")]
        [TestCase("./files/test.jpg")]
        [TestCase("./files/test.png")]
        [TestCase("./files/test.gif")]
        [TestCase("./files/test.tif")]
        [TestCase("./files/test.psd")]
        [TestCase("./files/test.pdf")]
        [TestCase("./files/test.doc")]
        [TestCase("./files/test.xml")]
        [TestCase("./files/test.zip")]
        [TestCase("./files/test.7z")]
        [TestCase("./files/test.bz2")]
        [TestCase("./files/test.gz")]
        [TestCase("./files/test-bom.xml")]
        [TestCase("./files/blob.mp3")]
        [TestCase("./files/test.wmf")]
        [TestCase("./files/test.ico")]
        [TestCase("./files/365-doc.docx")]
        public void IsTypeRecognizable_ShouldReturnTrueIfFileIsRecognized(string filePath)
        {
            using var fileStream = File.OpenRead(filePath);

            var expected = true;

            var actual = FileTypeValidator.IsTypeRecognizable(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("./files/test.bmp")]
        [TestCase("./files/test.jpg")]
        [TestCase("./files/test.png")]
        [TestCase("./files/test.gif")]
        [TestCase("./files/test.tif")]
        [TestCase("./files/test.psd")]
        [TestCase("./files/test.pdf")]
        [TestCase("./files/test.doc")]
        [TestCase("./files/test.xml")]
        [TestCase("./files/test.zip")]
        [TestCase("./files/test.7z")]
        [TestCase("./files/test.bz2")]
        [TestCase("./files/test.gz")]
        [TestCase("./files/test-bom.xml")]
        [TestCase("./files/blob.mp3")]
        [TestCase("./files/test.wmf")]
        [TestCase("./files/test.ico")]
        [TestCase("./files/365-doc.docx")]
        public async Task IsTypeRecognizableAsync_ShouldReturnTrueIfFileIsRecognized(string filePath)
        {
            using var fileStream = File.OpenRead(filePath);

            var actual = await FileTypeValidator.IsTypeRecognizableAsync(fileStream);

            Assert.AreEqual(expected: true, actual);
        }

        [Test]
        [TestCase("./files/test.bmp", Bitmap.TypeExtension)]
        [TestCase("./files/test.jpg", JointPhotographicExpertsGroup.TypeExtension)]
        [TestCase("./files/test.png", PortableNetworkGraphic.TypeExtension)]
        [TestCase("./files/test.gif", GraphicsInterchangeFormat87.TypeExtension)]
        [TestCase("./files/test.tif", TaggedImageFileFormat.TypeExtension)]
        [TestCase("./files/test.psd", PhotoshopDocumentFile.TypeExtension)]
        [TestCase("./files/test.pdf", PortableDocumentFormat.TypeExtension)]
        [TestCase("./files/test.doc", MicrosoftOfficeDocument.TypeExtension)]
        [TestCase("./files/test.xml", ExtensibleMarkupLanguage.TypeExtension)]
        [TestCase("./files/test.zip", ZipFile.TypeExtension)]
        [TestCase("./files/test.7z", SevenZipFile.TypeExtension)]
        [TestCase("./files/test.bz2", BZip2File.TypeExtension)]
        [TestCase("./files/test.gz", Gzip.TypeExtension)]
        [TestCase("./files/blob.mp3", Mp3.TypeExtension)]
        [TestCase("./files/test.wmf", WindowsMetaFileType.TypeExtension)]
        [TestCase("./files/test.ico", Icon.TypeExtension)]
        [TestCase("./files/365-doc.docx", MicrosoftOffice365Document.TypeExtension)]
        public void GetFileType_ShouldReturnFileExtension(string filePath, string expectedFileExtension)
        {
            using var fileStream = File.OpenRead(filePath);

            var actualFileTypeExtension = FileTypeValidator.GetFileType(fileStream).Extension;

            Assert.AreEqual(expectedFileExtension, actualFileTypeExtension);
        }

        [Test]
        [TestCase("./files/test.bmp", Bitmap.TypeExtension)]
        [TestCase("./files/test.jpg", JointPhotographicExpertsGroup.TypeExtension)]
        [TestCase("./files/test.png", PortableNetworkGraphic.TypeExtension)]
        [TestCase("./files/test.gif", GraphicsInterchangeFormat87.TypeExtension)]
        [TestCase("./files/test.tif", TaggedImageFileFormat.TypeExtension)]
        [TestCase("./files/test.psd", PhotoshopDocumentFile.TypeExtension)]
        [TestCase("./files/test.pdf", PortableDocumentFormat.TypeExtension)]
        [TestCase("./files/test.doc", MicrosoftOfficeDocument.TypeExtension)]
        [TestCase("./files/test.xml", ExtensibleMarkupLanguage.TypeExtension)]
        [TestCase("./files/test.zip", ZipFile.TypeExtension)]
        [TestCase("./files/test.7z", SevenZipFile.TypeExtension)]
        [TestCase("./files/test.bz2", BZip2File.TypeExtension)]
        [TestCase("./files/test.gz", Gzip.TypeExtension)]
        [TestCase("./files/blob.mp3", Mp3.TypeExtension)]
        [TestCase("./files/test.wmf", WindowsMetaFileType.TypeExtension)]
        [TestCase("./files/test.ico", Icon.TypeExtension)]
        [TestCase("./files/365-doc.docx", MicrosoftOffice365Document.TypeExtension)]
        public async Task GetFileTypeAsync_ShouldReturnFileExtension(string filePath, string expectedFileExtension)
        {
            using var fileStream = File.OpenRead(filePath);

            var actualFileTypeExtension = (await FileTypeValidator.GetFileTypeAsync(fileStream)).Extension;

            Assert.AreEqual(expectedFileExtension, actualFileTypeExtension);
        }

        [Test]
        [TestCase("./files/test.bmp", Bitmap.TypeName)]
        [TestCase("./files/test.jpg", JointPhotographicExpertsGroup.TypeName)]
        [TestCase("./files/test.png", PortableNetworkGraphic.TypeName)]
        [TestCase("./files/test.gif", GraphicsInterchangeFormat89.TypeName)]
        [TestCase("./files/test.tif", TaggedImageFileFormat.TypeName)]
        [TestCase("./files/test.psd", PhotoshopDocumentFile.TypeName)]
        [TestCase("./files/test.pdf", PortableDocumentFormat.TypeName)]
        [TestCase("./files/test.doc", MicrosoftOfficeDocument.TypeName)]
        [TestCase("./files/test.xml", ExtensibleMarkupLanguage.TypeName)]
        [TestCase("./files/test.zip", ZipFile.TypeName)]
        [TestCase("./files/test.7z", SevenZipFile.TypeName)]
        [TestCase("./files/test.bz2", BZip2File.TypeName)]
        [TestCase("./files/test.gz", Gzip.TypeName)]
        [TestCase("./files/blob.mp3", Mp3.TypeName)]
        [TestCase("./files/test.wmf", WindowsMetaFileType.TypeName)]
        [TestCase("./files/test.ico", Icon.TypeName)]
        [TestCase("./files/365-doc.docx", MicrosoftOffice365Document.TypeName)]
        public void GetFileType_ShouldReturnFileName(string filePath, string expectedFileTypeName)
        {
            using var fileStream = File.OpenRead(filePath);

            var actualFileTypeName = FileTypeValidator.GetFileType(fileStream).Name;

            Assert.AreEqual(expectedFileTypeName, actualFileTypeName);
        }

        [Test]
        [TestCase("./files/test.bmp", Bitmap.TypeName)]
        [TestCase("./files/test.jpg", JointPhotographicExpertsGroup.TypeName)]
        [TestCase("./files/test.png", PortableNetworkGraphic.TypeName)]
        [TestCase("./files/test.gif", GraphicsInterchangeFormat89.TypeName)]
        [TestCase("./files/test.tif", TaggedImageFileFormat.TypeName)]
        [TestCase("./files/test.psd", PhotoshopDocumentFile.TypeName)]
        [TestCase("./files/test.pdf", PortableDocumentFormat.TypeName)]
        [TestCase("./files/test.doc", MicrosoftOfficeDocument.TypeName)]
        [TestCase("./files/test.xml", ExtensibleMarkupLanguage.TypeName)]
        [TestCase("./files/test.zip", ZipFile.TypeName)]
        [TestCase("./files/test.7z", SevenZipFile.TypeName)]
        [TestCase("./files/test.bz2", BZip2File.TypeName)]
        [TestCase("./files/test.gz", Gzip.TypeName)]
        [TestCase("./files/blob.mp3", Mp3.TypeName)]
        [TestCase("./files/test.wmf", WindowsMetaFileType.TypeName)]
        [TestCase("./files/test.ico", Icon.TypeName)]
        [TestCase("./files/365-doc.docx", MicrosoftOffice365Document.TypeName)]
        public async Task GetFileTypeAsync_ShouldReturnFileName(string filePath, string expectedFileTypeName)
        {
            using var fileStream = File.OpenRead(filePath);

            var actualFileTypeName = (await FileTypeValidator.GetFileTypeAsync(fileStream)).Name;

            Assert.AreEqual(expectedFileTypeName, actualFileTypeName);
        }

        [Test]
        public void GetFileType_ShouldThrowArgumentNullExceptionIfStreamIsNull()
            => Assert.Catch<ArgumentNullException>(() => FileTypeValidator.GetFileType(null));

        public void GetFileTypeAsync_ShouldThrowArgumentNullExceptionIfStreamIsNull()
           => Assert.Catch<ArgumentNullException>(async () => await FileTypeValidator.GetFileTypeAsync(null));

        [Test]
        public void Is_ShouldThrowExceptionIfStreamIsNull()
            => Assert.Catch<ArgumentNullException>(() => FileTypeValidator.Is<Bitmap>(null));

        [Test]
        public void IsAsync_ShouldThrowExceptionIfStreamIsNull()
            => Assert.CatchAsync<ArgumentNullException>(async () => await FileTypeValidator.IsAsync<Bitmap>(null));

        [Test]
        public void Is_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = true;
            var actual = FileTypeValidator.Is<Bitmap>(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task IsAsync_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = true;
            var actual = await FileTypeValidator.IsAsync<Bitmap>(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Is_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = false;
            var actual = FileTypeValidator.Is<Gzip>(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task IsAsync_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = false;
            var actual = await FileTypeValidator.IsAsync<Gzip>(fileStream);

            Assert.AreEqual(expected, actual);
        }
    }
}