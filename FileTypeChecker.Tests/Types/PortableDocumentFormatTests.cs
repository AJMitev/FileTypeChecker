namespace FileTypeChecker.Tests.Types
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using FileTypeChecker.Types;
    using NUnit.Framework;

    [TestFixture]
    public class PortableDocumentFormatTests
    {
        private static readonly byte[] PdfHeader = { 0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x34 }; // "%PDF-1.4"

        private static MemoryStream BuildStream(int leadingJunk, int trailing)
        {
            var buffer = new byte[leadingJunk + PdfHeader.Length + trailing];
            for (var i = 0; i < leadingJunk; i++)
            {
                buffer[i] = 0xAA;
            }

            PdfHeader.CopyTo(buffer, leadingJunk);
            return new MemoryStream(buffer);
        }

        [Test]
        public void DoesMatchWith_ShouldReturnTrue_WhenMagicAtOffsetZero()
        {
            var subject = new PortableDocumentFormat();
            using var stream = BuildStream(leadingJunk: 0, trailing: 64);

            Assert.IsTrue(subject.DoesMatchWith(stream));
        }

        [Test]
        public void DoesMatchWith_ShouldReturnTrue_WhenMagicAppearsLaterWithinFirst1024Bytes()
        {
            var subject = new PortableDocumentFormat();
            using var stream = BuildStream(leadingJunk: 240, trailing: 256);

            Assert.IsTrue(subject.DoesMatchWith(stream));
        }

        [Test]
        public void DoesMatchWith_ShouldReturnTrue_WhenMagicEndsExactlyAtByte1024()
        {
            var subject = new PortableDocumentFormat();
            using var stream = BuildStream(leadingJunk: 1024 - PdfHeader.Length, trailing: 0);

            Assert.IsTrue(subject.DoesMatchWith(stream));
        }

        [Test]
        public void DoesMatchWith_ShouldReturnFalse_WhenMagicStartsBeyondFirst1024Bytes()
        {
            var subject = new PortableDocumentFormat();
            using var stream = BuildStream(leadingJunk: 1100, trailing: 64);

            Assert.IsFalse(subject.DoesMatchWith(stream));
        }

        [Test]
        public void DoesMatchWith_ShouldReturnFalse_WhenMagicIsAbsent()
        {
            var subject = new PortableDocumentFormat();
            using var stream = new MemoryStream(Enumerable.Repeat((byte)0xAA, 2048).ToArray());

            Assert.IsFalse(subject.DoesMatchWith(stream));
        }

        [Test]
        public void GetFileType_ShouldClassifyAsPdf_WhenMagicIsWrappedInAMultipartEnvelope()
        {
            using var stream = BuildMultipartEnvelope();

            var detected = FileTypeValidator.GetFileType(stream);

            Assert.AreEqual(PortableDocumentFormat.TypeName, detected.Name);
        }

        private static MemoryStream BuildMultipartEnvelope()
        {
            const string boundary = "181fee63-5203-4ea2-90a1-75747423b2db";
            const string pdfBody = "%PDF-1.4\n1 0 obj\n<< /Type /Catalog >>\nendobj\nstartxref\n0\n%%EOF";

            var envelope = new StringBuilder()
                .Append("--").Append(boundary).Append("\r\n")
                .Append("Content-Disposition: form-data; name=File; filename=report.pdf\r\n")
                .Append("\r\n")
                .Append(pdfBody)
                .Append("\r\n")
                .Append("--").Append(boundary).Append("--\r\n")
                .ToString();

            return new MemoryStream(Encoding.ASCII.GetBytes(envelope));
        }
    }
}
