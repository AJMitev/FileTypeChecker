namespace FileTypeChecker
{
    using FileTypeChecker.Extensions;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class MagicSequence : IEnumerable<byte>
    {
        private readonly byte[] data;
        private readonly int bytesToSkip;
        private readonly int startSkippingFrom;

        public MagicSequence(byte[] data) : this(data, 0, 0)
        { }

        public MagicSequence(byte[] data, int offset) : this(data, offset, 0)
        { }

        public MagicSequence(byte[] data, int bytesToSkip, int startSkippingFrom)
        {
            this.data = data;
            this.bytesToSkip = bytesToSkip;
            this.startSkippingFrom = startSkippingFrom;
        }

        public byte[] Bytes => this.GetBytes(data);

        public bool Equals(byte[] buffer) => this.GetBytes(buffer).SequenceEqual(Bytes);

        public IEnumerator<byte> GetEnumerator()
        {
            foreach (var magicByte in data)
            {
                yield return magicByte;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int CountMatchingBytes(byte[] buffer)
            => this.Bytes.CountMatchingBytes(this.GetBytes(buffer));

        private byte[] GetBytes(byte[] bytes)
        {
            var result = startSkippingFrom != 0
            ? this.TakeComparableSequence(bytes)
            : bytes.Skip(bytesToSkip)
                    .Take(bytes.Length - bytesToSkip)
                    .ToArray();

            return result;
        }

        private byte[] TakeComparableSequence(byte[] bytes)
        {
            var first = bytes.Take(startSkippingFrom - 1).ToList();
            var second = bytes.Skip(bytesToSkip + first.Count).Take(bytes.Length - (bytesToSkip + first.Count));
            first.AddRange(second);

            return first.ToArray();
        }
    }
}
