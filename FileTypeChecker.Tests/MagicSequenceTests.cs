namespace FileTypeChecker.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class MagicSequenceTests
    {
        [Test]
        [TestCase(new byte[] { 0x49, 0x44, 0x33 }, new byte[] { 0x49, 0x44, 0x33 }, 3)]
        [TestCase(new byte[] { 0x49, 0x44, 0x33, 0x44 }, new byte[] { 0x49, 0x44, 0x33 }, 3)]
        [TestCase(new byte[] { 0x49, 0x44, 0x33, 0x44 }, new byte[] { 0x44, 0x49, 0x44, 0x33 }, 0)]
        [TestCase(new byte[] { 0x44, 0x49, 0x44, 0x33, 0x44 }, new byte[] { 0x49, 0x44, 0x33 }, 0)]
        public void CountMatchingBytesShouldReturnNumbersOfMachtingBytes(byte[] magicBytes, byte[] compareBytes, int matching)
        {
            var sequence = new MagicSequence(magicBytes);
            var actual = sequence.CountMatchingBytes(compareBytes);

            Assert.AreEqual(matching, actual);
        }

        [Test]
        [TestCase(new byte[] { 0x52, 0x49, 0x46, 0x46, 0x00, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50, 0x56, 0x50, 0x38 }, new byte[] { 0x52, 0x49, 0x46, 0x46, 0x00, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50, 0x56, 0x50, 0x38 }, 11, 4, 5)]
        public void CountMatchingBytesShouldReturnNumbersOfMachtingBytesWhenThereIsSkipInTheMiddle(byte[] magicBytes, byte[] compareBytes, int matching, int bytesToSkip, int startFrom)
        {
            var sequence = new MagicSequence(magicBytes, bytesToSkip, startFrom);
            var actual = sequence.CountMatchingBytes(compareBytes);

            Assert.AreEqual(matching, actual);
        }

    }
}
