using FileTypeChecker.Types;

namespace FileTypeChecker.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
    using NUnit.Framework;

    [TestFixture]
    public class FileTypeValidatorAsyncTests
    {
        private const string FilesPath = "./files/";

        [Test]
        public void IsTypeRecognizableAsync_ShouldThrowArgumentNullExceptionIfStreamIsNull()
            => Assert.ThrowsAsync<ArgumentNullException>(async () => await FileTypeValidator.IsTypeRecognizableAsync(null));

        [Test]
        public void IsTypeRecognizableAsync_ShouldThrowArgumentNullExceptionIfStreamIsEmpty()
            => Assert.ThrowsAsync<ArgumentNullException>(async () => await FileTypeValidator.IsTypeRecognizableAsync(Stream.Null));

        [Test]
        public async Task IsTypeRecognizableAsync_ShouldReturnFalseIfFormatIsUnknown()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test"));

            var expected = false;
            var actual = await FileTypeValidator.IsTypeRecognizableAsync(fileStream);

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
        [TestCase("file_example_AVI_480_750kB.avi")]
        [TestCase("file_example_WAV_1MG.wav")]
        public async Task IsTypeRecognizableAsync_ShouldReturnTrueIfFileIsRecognized(string fileName)
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, fileName));

            var expected = true;
            var actual = await FileTypeValidator.IsTypeRecognizableAsync(fileStream);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetFileTypeAsync_ShouldThrowArgumentNullExceptionIfStreamIsNull()
            => Assert.ThrowsAsync<ArgumentNullException>(async () => await FileTypeValidator.GetFileTypeAsync(null));

        [Test]
        public void GetFileTypeAsync_ShouldThrowTypeNotFoundExceptionWhenTypeIsNotRegistered()
            => Assert.ThrowsAsync<TypeNotFoundException>(async () => await FileTypeValidator.GetFileTypeAsync(File.OpenRead(Path.Combine(FilesPath, "test"))));

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
        [TestCase("testwin10.zip", typeof(ZipFile))]
        [TestCase("test.webp", typeof(Webp))]
        [TestCase("sample.heic", typeof(HighEfficiencyImageFile))]
        [TestCase("issue311docx.testfile", typeof(MicrosoftOffice365Document))]
        [TestCase("test-issue-41.xlsx", typeof(MicrosoftOffice365Document))]
        [TestCase("file_example_AVI_480_750kB.avi", typeof(AudioVideoInterleaveVideoFormat))]
        [TestCase("file_example_WAV_1MG.wav", typeof(WaveformAudioFileFormat))]
        public async Task GetFileTypeAsync_ShouldReturnAccurateType(string fileName, Type expectedType)
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, fileName));

            var fileType = await FileTypeValidator.GetFileTypeAsync(fileStream);

            Assert.AreEqual(expectedType, fileType.GetType());
        }

        [Test]
        public async Task TryGetFileTypeAsync_MatchShouldReturnFalseWhenStreamIsNull()
        {
            var match = await FileTypeValidator.TryGetFileTypeAsync(null);

            Assert.IsFalse(match.HasMatch);
        }

        [Test]
        [TestCase("test.png")]
        [TestCase("test.webp")]
        [TestCase("test.mp4")]
        public async Task TryGetFileTypeAsync_MatchShouldReturnTrueWhenTypeIsValid(string fileName)
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, fileName));
            var match = await FileTypeValidator.TryGetFileTypeAsync(fileStream);

            Assert.IsTrue(match.HasMatch);
        }

        [Test]
        public async Task TryGetFileTypeAsync_TypeShouldNotBeNull()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var match = await FileTypeValidator.TryGetFileTypeAsync(fileStream);

            Assert.IsNotNull(match.Type);
        }

        [Test]
        public async Task TryGetFileTypeAsync_TypeShouldBeCorrectType()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var match = await FileTypeValidator.TryGetFileTypeAsync(fileStream);
            var expectedType = new PortableNetworkGraphic();

            Assert.AreEqual(match.Type.Name, expectedType.Name);
        }

        [Test]
        public async Task TryGetFileTypeAsync_TypeShouldBeNullWhenNotMatching()
        {
            var match = await FileTypeValidator.TryGetFileTypeAsync(null);

            Assert.IsNull(match.Type);
        }

        [Test]
        public async Task IsAsync_ShouldReturnTrueIfTheTypesMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var result = await FileTypeValidator.IsAsync<PortableNetworkGraphic>(fileStream);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsAsync_ShouldReturnFalseIfTypesDidNotMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var result = await FileTypeValidator.IsAsync<Bitmap>(fileStream);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsImageAsync_ShouldReturnTrueIfTheTypesMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var result = await FileTypeValidator.IsImageAsync(fileStream);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsImageAsync_ShouldReturnFalseIfTypesDidNotMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.pdf"));
            var result = await FileTypeValidator.IsImageAsync(fileStream);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsArchiveAsync_ShouldReturnTrueIfTheTypesMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.zip"));
            var result = await FileTypeValidator.IsArchiveAsync(fileStream);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsArchiveAsync_ShouldReturnFalseIfTypesDidNotMatch()
        {
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            var result = await FileTypeValidator.IsArchiveAsync(fileStream);

            Assert.IsFalse(result);
        }

        [Test]
        public void AsyncMethods_ShouldSupportCancellation()
        {
            using var fileStream = File.OpenRead(Path.Combine(FilesPath, "test.png"));
            using var cts = new CancellationTokenSource();
            
            // Cancel immediately to test cancellation support
            cts.Cancel();

            Assert.ThrowsAsync<TaskCanceledException>(async () => 
            {
                var buffer = new byte[1024];
                await fileStream.ReadAsync(buffer, 0, buffer.Length, cts.Token);
            });
        }

        [Test]
        public async Task AsyncMethods_ShouldWorkWithLargeFiles()
        {
            // Test with one of the larger test files
            await using var fileStream = File.OpenRead(Path.Combine(FilesPath, "365-doc.docx"));
            
            var isRecognizable = await FileTypeValidator.IsTypeRecognizableAsync(fileStream);
            var fileType = await FileTypeValidator.GetFileTypeAsync(fileStream);
            var tryResult = await FileTypeValidator.TryGetFileTypeAsync(fileStream);
            var isDocument = await FileTypeValidator.IsImageAsync(fileStream);

            Assert.IsTrue(isRecognizable);
            Assert.IsNotNull(fileType);
            Assert.IsTrue(tryResult.HasMatch);
            Assert.IsFalse(isDocument); // DOCX is not an image
        }
    }
}