using FileTypeChecker.Types;

namespace FileTypeChecker.Tests
{
    using System;
    using System.IO;
    using Exceptions;
    using Types;
    using NUnit.Framework;
    using System.Reflection;

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
        [TestCase("test.mp4")]
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
        [TestCase("test-offset.mp4", M4V.TypeExtension)]
        public void TryGetFileType_ShouldReturnFileExtension(string filePath, string expectedFileExtension)
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, filePath));
            var match = FileTypeValidator.TryGetFileType(fileStream);

            Assert.AreEqual(expectedFileExtension, match.Type?.Extension);
        }

        [Test]
        [TestCase("test.png")]
        [TestCase("test.webp")]
        [TestCase("test.mp4")]
        public void TryGetFileType_MatchShouldReturnTrueWhenTypeIsValid(string fileName)
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, fileName));
            var match = FileTypeValidator.TryGetFileType(fileStream);

            Assert.IsTrue(match.HasMatch);
        }

        [Test]
        public void TryGetFileType_MatchShouldReturnFalseWhenStreamIsNull()
        {
            var match = FileTypeValidator.TryGetFileType(null);

            Assert.IsFalse(match.HasMatch);
        }

        [Test]
        public void TryGetFileType_TypeShouldNotBeNull()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var match = FileTypeValidator.TryGetFileType(fileStream);

            Assert.IsNotNull(match.Type);
        }


        [Test]
        public void TryGetFileType_TypeShouldBeCorrectType()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var match = FileTypeValidator.TryGetFileType(fileStream);
            var expectedType = new PortableNetworkGraphic();

            Assert.AreEqual(match.Type.Name, expectedType.Name);
        }

        [Test]
        public void TryGetFileType_TypeShouldBeNullWhenNotMatching()
        {
            var match = FileTypeValidator.TryGetFileType(null);

            Assert.IsNull(match.Type);
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
        public void GetFileType_ShouldThrowTypeNotFoundExceptionWhenTypeIsNotRegistered()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test"));
            Assert.Throws<TypeNotFoundException>(() => FileTypeValidator.GetFileType(fileStream));
        }

        [Test]
        public void GetFileType_ShouldThrowArgumentNullExceptionIfStreamIsNull()
            => Assert.Catch<ArgumentNullException>(() => FileTypeValidator.GetFileType(fileContent: null));

        [Test]
        public void Is_ShouldThrowExceptionIfStreamIsNull()
            => Assert.Catch<ArgumentNullException>(() => FileTypeValidator.Is<Bitmap>(Stream.Null));

        [Test]
        public void Is_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.bmp"));
            var actual = FileTypeValidator.Is<Bitmap>(fileStream);

            Assert.IsTrue(actual);
        }

        [Test]
        public void Is_ShouldReturnFalseIfTypesDidNotMatch()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.bmp"));
            var actual = FileTypeValidator.Is<Gzip>(fileStream);

            Assert.IsFalse(actual);
        }

        [Test]
        [TestCase("test.bmp", typeof(Bitmap))]
        [TestCase("test.jpg", typeof(JointPhotographicExpertsGroup))]
        [TestCase("test.png", typeof(PortableNetworkGraphic))]
        [TestCase("test.gif", typeof(GraphicsInterchangeFormat89))]
        [TestCase("test.tif", typeof(TaggedImageFileFormat))]
        [TestCase("test.psd", typeof(PhotoshopDocumentFile))]
        [TestCase("test.pdf", typeof(PortableDocumentFormat))]
        [TestCase("test.doc", typeof(MicrosoftOfficeDocument))]
        [TestCase("test.xml", typeof(ExtensibleMarkupLanguage))]
        [TestCase("test.zip", typeof(ZipFile))]
        [TestCase("test.7z", typeof(SevenZipFile))]
        [TestCase("test.bz2", typeof(BZip2File))]
        [TestCase("test.gz", typeof(Gzip))]
        [TestCase("blob.mp3", typeof(MpegAudio))]
        [TestCase("test.wmf", typeof(WindowsMetaFileType))]
        [TestCase("test.ico", typeof(Icon))]
        [TestCase("365-doc.docx", typeof(MicrosoftOffice365Document))]
        [TestCase("365-doc-empty.docx", typeof(MicrosoftOffice365Document))]
        [TestCase("365-issue311.docx", typeof(MicrosoftOffice365Document))]
        [TestCase("testwin10.zip", typeof(ZipFile))]
        [TestCase("test.webp", typeof(Webp))]
        [TestCase("sample.heic", typeof(HighEfficiencyImageFile))]
        public void GetFileType_ShouldReturnAccurateTypeWhenUsingBytes(string fileName, Type expectedType)
        {
            var buffer = GetFileBytes(fileName);
            var type = FileTypeValidator.GetFileType(buffer).GetType();

            Assert.AreEqual(expectedType, type);
        }

        [Test]
        [TestCase("arbitrary_csv_1.txt", typeof(ArbitraryCsv1FileType))]
        [TestCase("arbitrary_csv_2.txt", typeof(ArbitraryCsv2FileType))]
        public void GetFileType_ShouldUseTheMinimalBufferSizeWhenUsingStream(string fileName, Type expectedType)
        {
            using var stream = File.OpenRead(Path.Combine(FilesPath, fileName));
            var type = FileTypeValidator.GetFileType(stream).GetType();
            Assert.AreEqual(expectedType, type);
        }

        private static byte[] GetFileBytes(string fileName)
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, fileName));
            var buffer = new byte[24];
            fileStream.Position = 0;
            fileStream.Read(buffer, 0, buffer.Length);
            return buffer;
        }

    }
}