namespace FileTypeChecker.Tests
{
    using System;
    using System.IO;
    using FileTypeChecker.Types;
    using NUnit.Framework;

    [TestFixture]
    public class FileTypeValidatorTests
    {
        [Test]
        public void IsTypeRecognizable_ShouldThrowArgumentNullExceptionIfStreamIsNull()
            => Assert.Catch<ArgumentNullException>(() => FileTypeValidator.IsTypeRecognizable(null));

        [Test]
        public void IsTypeRecognizable_ShouldReturnFalseIfFormatIsUnknown()
        {
            using var fileStream = File.OpenRead("./files/test");

            var expected = false;

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
        public void IsTypeRecognizable_ShouldReturnTrueIfFileIsRecognized(string filePath)
        {
            using var fileStream = File.OpenRead(filePath);

            var expected = true;

            var actual = FileTypeValidator.IsTypeRecognizable(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("./files/test.bmp", FileExtension.Bitmap)]
        [TestCase("./files/test.jpg", FileExtension.Jpg)]
        [TestCase("./files/test.png", FileExtension.Png)]
        [TestCase("./files/test.gif", FileExtension.Gif)]
        [TestCase("./files/test.tif", FileExtension.Tif)]
        [TestCase("./files/test.psd", FileExtension.Psd)]
        [TestCase("./files/test.pdf", FileExtension.Pdf)]
        [TestCase("./files/test.doc", FileExtension.Doc)]
        [TestCase("./files/test.xml", FileExtension.Xml)]
        [TestCase("./files/test.zip", FileExtension.Zip)]
        [TestCase("./files/test.7z", FileExtension.SevenZ)]
        [TestCase("./files/test.bz2", FileExtension.Bz2)]
        [TestCase("./files/test.gz", FileExtension.Gz)]
        [TestCase("./files/blob.mp3", FileExtension.Mp3)]
        [TestCase("./files/test.wmf", FileExtension.Wmf)]
        [TestCase("./files/test.ico", FileExtension.Ico)]
        public void GetFileType_ShouldReturnFileExtension(string filePath, string expectedFileExtension)
        {
            using var fileStream = File.OpenRead(filePath);

            var actualFileTypeExtension = FileTypeValidator.GetFileType(fileStream).Extension;

            Assert.AreEqual(expectedFileExtension, actualFileTypeExtension);
        }

        [Test]
        [TestCase("./files/test.bmp", "Bitmap")]
        [TestCase("./files/test.jpg", "JPEG")]
        [TestCase("./files/test.png", "Portable Network Graphic")]
        [TestCase("./files/test.gif", "Graphics Interchange Format 89a")]
        [TestCase("./files/test.tif", "Tagged Image File Format")]
        [TestCase("./files/test.psd", "Photoshop Document file")]
        [TestCase("./files/test.pdf", "Portable Document Format")]
        [TestCase("./files/test.doc", "Microsoft Office Document 97-2003")]
        [TestCase("./files/test.xml", "eXtensible Markup Language")]
        [TestCase("./files/test.zip", "ZIP file")]
        [TestCase("./files/test.7z", "7-Zip File Format")]
        [TestCase("./files/test.bz2", "BZIP2 file")]
        [TestCase("./files/test.gz", "GZIP compressed file")]
        [TestCase("./files/test.mp4", "MP4 file")]
        [TestCase("./files/blob.mp3", "MPEG audio file frame synch pattern")]
        [TestCase("./files/test.wmf", "Windows Meta File")]
        [TestCase("./files/test.ico", "Icon")]
        public void GetFileType_ShouldReturnFileName(string filePath, string expectedFileTypeName)
        {
            using var fileStream = File.OpenRead(filePath);

            var actualFileTypeName = FileTypeValidator.GetFileType(fileStream).Name;

            Assert.AreEqual(expectedFileTypeName, actualFileTypeName);
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
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = true;
            var actual = FileTypeValidator.Is<Bitmap>(fileStream);

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
    }
}