namespace FileTypeChecker.Tests
{
    using System;
    using System.IO;
    using FileTypeChecker.Exceptions;
    using FileTypeChecker.Types;
    using NUnit.Framework;

    [TestFixture]
    public class FileTypeValidatorTests
    {
        private const string FilesPath = "./files/";

        [Test]
        public void IsTypeRecognizable_ShouldThrowArgumentNullExceptionIfStreamIsNull()
            => Assert.Catch<ArgumentNullException>(() => FileTypeValidator.IsTypeRecognizable(null as Stream));

        [Test]
        public void IsTypeRecognizable_ShouldThrowArgumentNullExceptionIfStreamIsEmpty()
            => Assert.Catch<ArgumentNullException>(() => FileTypeValidator.IsTypeRecognizable(Stream.Null));

        [Test]
        public void IsTypeRecognizable_ShouldReturnFalseIfFormatIsUnknown()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test"));

            var expected = false;

            var actual = FileTypeValidator.IsTypeRecognizable(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("test.bmp")]
        [TestCase("test.jpg")]
        [TestCase("test.png")]
        [TestCase("test.gif")]
        [TestCase("test.tif")]
        [TestCase("test.psd")]
        [TestCase("test.pdf")]
        [TestCase("test.doc")]
        [TestCase("test.xml")]
        [TestCase("test.zip")]
        [TestCase("test.7z")]
        [TestCase("test.bz2")]
        [TestCase("test.gz")]
        [TestCase("test-bom.xml")]
        [TestCase("blob.mp3")]
        [TestCase("test.wmf")]
        [TestCase("test.ico")]
        [TestCase("365-doc.docx")]
        [TestCase("testwin10.zip")]
        [TestCase("test.webp")]
        [TestCase("sample.heic")]
        public void IsTypeRecognizable_ShouldReturnTrueIfFileIsRecognized(string filePath)
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, filePath));

            var expected = true;

            var actual = FileTypeValidator.IsTypeRecognizable(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("test.bmp", Bitmap.TypeExtension)]
        [TestCase("test.jpg", JointPhotographicExpertsGroup.TypeExtension)]
        [TestCase("test.png", PortableNetworkGraphic.TypeExtension)]
        [TestCase("test.gif", GraphicsInterchangeFormat87.TypeExtension)]
        [TestCase("test.tif", TaggedImageFileFormat.TypeExtension)]
        [TestCase("test.psd", PhotoshopDocumentFile.TypeExtension)]
        [TestCase("test.pdf", PortableDocumentFormat.TypeExtension)]
        [TestCase("test.doc", MicrosoftOfficeDocument.TypeExtension)]
        [TestCase("test.xml", ExtensibleMarkupLanguage.TypeExtension)]
        [TestCase("test.zip", ZipFile.TypeExtension)]
        [TestCase("test.7z", SevenZipFile.TypeExtension)]
        [TestCase("test.bz2", BZip2File.TypeExtension)]
        [TestCase("test.gz", Gzip.TypeExtension)]
        [TestCase("blob.mp3", Mp3.TypeExtension)]
        [TestCase("test.wmf", WindowsMetaFileType.TypeExtension)]
        [TestCase("test.ico", Icon.TypeExtension)]
        [TestCase("365-doc.docx", MicrosoftOffice365Document.TypeExtension)]
        [TestCase("testwin10.zip", ZipFile.TypeExtension)]
        [TestCase("test.webp", Webp.TypeExtension)]
        [TestCase("sample.heic", HighEfficiencyImageFile.TypeExtension)]
        public void GetFileType_ShouldReturnFileExtension(string filePath, string expectedFileExtension)
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, filePath));

            var actualFileTypeExtension = FileTypeValidator.GetFileType(fileStream).Extension;

            Assert.AreEqual(expectedFileExtension, actualFileTypeExtension);
        }
        
        [Test]
        [TestCase("test.bmp", Bitmap.TypeExtension)]
        [TestCase("test.jpg", JointPhotographicExpertsGroup.TypeExtension)]
        [TestCase("test.png", PortableNetworkGraphic.TypeExtension)]
        [TestCase("test.gif", GraphicsInterchangeFormat87.TypeExtension)]
        [TestCase("test.tif", TaggedImageFileFormat.TypeExtension)]
        [TestCase("test.psd", PhotoshopDocumentFile.TypeExtension)]
        [TestCase("test.pdf", PortableDocumentFormat.TypeExtension)]
        [TestCase("test.doc", MicrosoftOfficeDocument.TypeExtension)]
        [TestCase("test.xml", ExtensibleMarkupLanguage.TypeExtension)]
        [TestCase("test.zip", ZipFile.TypeExtension)]
        [TestCase("test.7z", SevenZipFile.TypeExtension)]
        [TestCase("test.bz2", BZip2File.TypeExtension)]
        [TestCase("test.gz", Gzip.TypeExtension)]
        [TestCase("blob.mp3", Mp3.TypeExtension)]
        [TestCase("test.wmf", WindowsMetaFileType.TypeExtension)]
        [TestCase("test.ico", Icon.TypeExtension)]
        [TestCase("365-doc.docx", MicrosoftOffice365Document.TypeExtension)]
        [TestCase("testwin10.zip", ZipFile.TypeExtension)]
        [TestCase("test.webp", Webp.TypeExtension)]
        [TestCase("sample.heic", HighEfficiencyImageFile.TypeExtension)]
        public void TryGetFileType_ShouldReturnFileExtension(string filePath, string expectedFileExtension)
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, filePath));

            Assert.True(FileTypeValidator.TryGetFileType(fileStream, out var fileType));
            var actualFileTypeExtension = fileType.Extension;

            Assert.AreEqual(expectedFileExtension, actualFileTypeExtension);
        }

        [Test]
        [TestCase("test.bmp", Bitmap.TypeName)]
        [TestCase("test.jpg", JointPhotographicExpertsGroup.TypeName)]
        [TestCase("test.png", PortableNetworkGraphic.TypeName)]
        [TestCase("test.gif", GraphicsInterchangeFormat89.TypeName)]
        [TestCase("test.tif", TaggedImageFileFormat.TypeName)]
        [TestCase("test.psd", PhotoshopDocumentFile.TypeName)]
        [TestCase("test.pdf", PortableDocumentFormat.TypeName)]
        [TestCase("test.doc", MicrosoftOfficeDocument.TypeName)]
        [TestCase("test.xml", ExtensibleMarkupLanguage.TypeName)]
        [TestCase("test.zip", ZipFile.TypeName)]
        [TestCase("test.7z", SevenZipFile.TypeName)]
        [TestCase("test.bz2", BZip2File.TypeName)]
        [TestCase("test.gz", Gzip.TypeName)]
        [TestCase("blob.mp3", Mp3.TypeName)]
        [TestCase("test.wmf", WindowsMetaFileType.TypeName)]
        [TestCase("test.ico", Icon.TypeName)]
        [TestCase("365-doc.docx", MicrosoftOffice365Document.TypeName)]
        [TestCase("testwin10.zip", ZipFile.TypeName)]
        [TestCase("test.webp", Webp.TypeName)]
        [TestCase("sample.heic", HighEfficiencyImageFile.TypeName)]
        public void GetFileType_ShouldReturnFileName(string filePath, string expectedFileTypeName)
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, filePath));

            var actualFileTypeName = FileTypeValidator.GetFileType(fileStream).Name;

            Assert.AreEqual(expectedFileTypeName, actualFileTypeName);
        }
        
        [Test]
        [TestCase("test.bmp", Bitmap.TypeName)]
        [TestCase("test.jpg", JointPhotographicExpertsGroup.TypeName)]
        [TestCase("test.png", PortableNetworkGraphic.TypeName)]
        [TestCase("test.gif", GraphicsInterchangeFormat89.TypeName)]
        [TestCase("test.tif", TaggedImageFileFormat.TypeName)]
        [TestCase("test.psd", PhotoshopDocumentFile.TypeName)]
        [TestCase("test.pdf", PortableDocumentFormat.TypeName)]
        [TestCase("test.doc", MicrosoftOfficeDocument.TypeName)]
        [TestCase("test.xml", ExtensibleMarkupLanguage.TypeName)]
        [TestCase("test.zip", ZipFile.TypeName)]
        [TestCase("test.7z", SevenZipFile.TypeName)]
        [TestCase("test.bz2", BZip2File.TypeName)]
        [TestCase("test.gz", Gzip.TypeName)]
        [TestCase("blob.mp3", Mp3.TypeName)]
        [TestCase("test.wmf", WindowsMetaFileType.TypeName)]
        [TestCase("test.ico", Icon.TypeName)]
        [TestCase("365-doc.docx", MicrosoftOffice365Document.TypeName)]
        [TestCase("testwin10.zip", ZipFile.TypeName)]
        [TestCase("test.webp", Webp.TypeName)]
        [TestCase("sample.heic", HighEfficiencyImageFile.TypeName)]
        public void TryGetFileType_ShouldReturnFileName(string filePath, string expectedFileTypeName)
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, filePath));

            Assert.True(FileTypeValidator.TryGetFileType(fileStream, out var fileType));
            var actualFileTypeName = fileType.Name;

            Assert.AreEqual(expectedFileTypeName, actualFileTypeName);
        }

        [Test]
        public void GetFileType_ShouldThrowTypeNotFoundExceptionWhenTypeIsNotRegistered()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test"));
            Assert.Throws<TypeNotFoundException>(() => FileTypeValidator.GetFileType(fileStream));
        }
        
        [Test]
        public void TryGetFileType_ShouldReturnFalseWhenTypeIsNotRegistered()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test"));
            Assert.False(FileTypeValidator.TryGetFileType(fileStream, out var fileType));
            Assert.Null(fileType);
        }

        [Test]
        public void GetFileType_ShouldThrowArgumentNullExceptionIfStreamIsNull()
            => Assert.Catch<ArgumentNullException>(() => FileTypeValidator.GetFileType(null));

        [Test]
        public void Is_ShouldThrowExceptionIfStreamIsNull()
            => Assert.Catch<ArgumentNullException>(() => FileTypeValidator.Is<Bitmap>(null));

        [Test]
        public void Is_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.bmp"));
            var expected = true;
            var actual = FileTypeValidator.Is<Bitmap>(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Is_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.bmp"));
            var expected = false;
            var actual = FileTypeValidator.Is<Gzip>(fileStream);

            Assert.AreEqual(expected, actual);
        }
    }
}