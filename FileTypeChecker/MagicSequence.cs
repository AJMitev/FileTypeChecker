namespace FileTypeChecker
{
    using FileTypeChecker.Exceptions;
    using FileTypeChecker.Extensions;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed class MagicSequence : IEnumerable<byte>
    {
        private readonly byte[] data;
        private readonly int bytesToSkip;
        private readonly int indexToStart;

        public MagicSequence(byte[] data) : this(data, 0, 0)
        { }

        public MagicSequence(byte[] data, int offset) : this(data, offset, 0)
        { }

        public MagicSequence(byte[] data, int bytesToSkip, int indexToStart)
        {
            this.data = data;
            this.bytesToSkip = bytesToSkip;
            this.indexToStart = indexToStart;
        }

        public ReadOnlyCollection<byte> Bytes => this.GetBytes(data);

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
        {
            if (buffer is null || buffer.Length == 0)
                throw new InvalidInputException("The byte array should not be null nor empty!");

            return this.Bytes.CountMatchingBytes(this.GetBytes(buffer));
        }

        private ReadOnlyCollection<byte> GetBytes(byte[] bytes)
        {
            if (bytes is null || bytes.Length == 0)
                throw new InvalidInputException("The byte array should not be null nor empty!");

            var result = indexToStart != 0
            ? this.TakeComparableSequence(bytes)
            : bytes.Skip(bytesToSkip)
                    .Take(bytes.Length - bytesToSkip)
                    .ToArray();

            return new ReadOnlyCollection<byte>(result);
        }

        private byte[] TakeComparableSequence(byte[] bytes)
        {
            var first = bytes.Take(indexToStart)
                .ToList();

            var second = bytes.Skip(bytesToSkip + first.Count)
                .Take(bytes.Length - (bytesToSkip + first.Count));

            first.AddRange(second);
            return first.ToArray();
        }
    }
}
