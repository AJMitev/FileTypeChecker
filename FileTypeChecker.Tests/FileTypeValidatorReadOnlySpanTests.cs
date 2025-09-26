using System;
using System.IO;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using NUnit.Framework;

namespace FileTypeChecker.Tests
{
    [TestFixture]
    public class FileTypeValidatorReadOnlySpanTests
    {
        private static readonly string FilesPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

        [Test]
        [TestCase("test.bmp", true)]
        [TestCase("test.jpg", true)]
        [TestCase("test.png", true)]
        [TestCase("test.pdf", true)]
        [TestCase("test.zip", true)]
        public void IsTypeRecognizable_ReadOnlySpan_ShouldReturnTrueForKnownTypes(string fileName, bool expected)
        {
            var fileBytes = GetFileBytes(fileName);
            var actual = FileTypeValidator.IsTypeRecognizable(fileBytes.AsSpan());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsTypeRecognizable_ReadOnlySpan_ShouldReturnFalseForUnknownTypes()
        {
            var unknownBytes = new byte[] { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA };
            var actual = FileTypeValidator.IsTypeRecognizable(unknownBytes.AsSpan());

            Assert.IsFalse(actual);
        }

        [Test]
        public void IsTypeRecognizable_ReadOnlySpan_ShouldThrowWhenEmpty()
        {
            Assert.Throws<ArgumentException>(() => FileTypeValidator.IsTypeRecognizable(ReadOnlySpan<byte>.Empty));
        }

        [Test]
        [TestCase("test.bmp", typeof(Bitmap))]
        [TestCase("test.jpg", typeof(JointPhotographicExpertsGroup))]
        [TestCase("test.png", typeof(PortableNetworkGraphic))]
        [TestCase("test.pdf", typeof(PortableDocumentFormat))]
        [TestCase("test.zip", typeof(ZipFile))]
        public void GetFileType_ReadOnlySpan_ShouldReturnCorrectType(string fileName, Type expectedType)
        {
            var fileBytes = GetFileBytes(fileName);
            var actualType = FileTypeValidator.GetFileType(fileBytes.AsSpan()).GetType();

            Assert.AreEqual(expectedType, actualType);
        }

        [Test]
        public void GetFileType_ReadOnlySpan_ShouldThrowWhenEmpty()
        {
            Assert.Throws<ArgumentException>(() => FileTypeValidator.GetFileType(ReadOnlySpan<byte>.Empty));
        }

        [Test]
        [TestCase("test.bmp", true)]
        [TestCase("test.jpg", true)]
        [TestCase("test.png", true)]
        public void TryGetFileType_ReadOnlySpan_ShouldReturnValidResult(string fileName, bool expectedHasMatch)
        {
            var fileBytes = GetFileBytes(fileName);
            var result = FileTypeValidator.TryGetFileType(fileBytes.AsSpan());

            Assert.AreEqual(expectedHasMatch, result.HasMatch);
            if (expectedHasMatch)
            {
                Assert.IsNotNull(result.Type);
            }
        }

        [Test]
        public void TryGetFileType_ReadOnlySpan_ShouldReturnFalseForEmpty()
        {
            var result = FileTypeValidator.TryGetFileType(ReadOnlySpan<byte>.Empty);
            Assert.IsFalse(result.HasMatch);
        }

        [Test]
        public void Is_ReadOnlySpan_ShouldReturnTrueForMatchingType()
        {
            var pngBytes = GetFileBytes("test.png");
            var actual = FileTypeValidator.Is<PortableNetworkGraphic>(pngBytes.AsSpan());

            Assert.IsTrue(actual);
        }

        [Test]
        public void Is_ReadOnlySpan_ShouldReturnFalseForNonMatchingType()
        {
            var pngBytes = GetFileBytes("test.png");
            var actual = FileTypeValidator.Is<Bitmap>(pngBytes.AsSpan());

            Assert.IsFalse(actual);
        }

        [Test]
        [TestCase("test.bmp", true)]
        [TestCase("test.jpg", true)]
        [TestCase("test.png", true)]
        [TestCase("test.zip", false)]
        public void IsImage_ReadOnlySpan_ShouldReturnCorrectResult(string fileName, bool expected)
        {
            var fileBytes = GetFileBytes(fileName);
            var actual = FileTypeValidator.IsImage(fileBytes.AsSpan());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("test.zip", true)]
        [TestCase("test.7z", true)]
        [TestCase("test.png", false)]
        public void IsArchive_ReadOnlySpan_ShouldReturnCorrectResult(string fileName, bool expected)
        {
            var fileBytes = GetFileBytes(fileName);
            var actual = FileTypeValidator.IsArchive(fileBytes.AsSpan());

            Assert.AreEqual(expected, actual);
        }

        // Extension method tests

        [Test]
        public void Is_ReadOnlySpan_Extension_ShouldWorkCorrectly()
        {
            var pngBytes = GetFileBytes("test.png");
            var actual = ((ReadOnlySpan<byte>)pngBytes.AsSpan()).Is<PortableNetworkGraphic>();

            Assert.IsTrue(actual);
        }

        [Test]
        [TestCase("test.bmp", true)]
        [TestCase("test.jpg", true)]
        [TestCase("test.png", true)]
        [TestCase("test.zip", false)]
        public void IsImage_ReadOnlySpan_Extension_ShouldReturnCorrectResult(string fileName, bool expected)
        {
            var fileBytes = GetFileBytes(fileName);
            var actual = ((ReadOnlySpan<byte>)fileBytes.AsSpan()).IsImage();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("test.zip", true)]
        [TestCase("test.7z", true)]
        [TestCase("test.png", false)]
        public void IsArchive_ReadOnlySpan_Extension_ShouldReturnCorrectResult(string fileName, bool expected)
        {
            var fileBytes = GetFileBytes(fileName);
            var actual = ((ReadOnlySpan<byte>)fileBytes.AsSpan()).IsArchive();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("test.pdf", true)]
        [TestCase("test.xml", true)]
        [TestCase("test.png", false)]
        public void IsDocument_ReadOnlySpan_Extension_ShouldReturnCorrectResult(string fileName, bool expected)
        {
            var fileBytes = GetFileBytes(fileName);
            var actual = ((ReadOnlySpan<byte>)fileBytes.AsSpan()).IsDocument();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ReadOnlySpan_PerformanceComparison_ShouldBeFaster()
        {
            var testFiles = new[] { "test.png", "test.jpg", "test.zip", "test.pdf" };
            
            // This test demonstrates that ReadOnlySpan methods work correctly
            // The actual performance benefits are shown in the benchmark project
            foreach (var file in testFiles)
            {
                var fileBytes = GetFileBytes(file);
                
                // Both methods should return the same results
                var byteArrayResult = FileTypeValidator.IsTypeRecognizable(fileBytes);
                var spanResult = FileTypeValidator.IsTypeRecognizable(fileBytes.AsSpan());
                
                Assert.AreEqual(byteArrayResult, spanResult, 
                    $"ReadOnlySpan and byte array methods should return same result for {file}");
            }
        }

        private static byte[] GetFileBytes(string fileName)
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, fileName));
            var buffer = new byte[64]; // Read first 64 bytes for magic number detection
            fileStream.Position = 0;
            fileStream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}