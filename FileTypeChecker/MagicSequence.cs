namespace FileTypeChecker
{
    using Exceptions;
    using Extensions;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed class MagicSequence : IEnumerable<byte>
    {
        private readonly byte[] _data;
        private readonly int _bytesToSkip;
        private readonly int _indexToStart;

        public int Length => _data.Length;

        public MagicSequence(byte[] data, int offset) : this(data, offset, 0)
        { }

        public MagicSequence(byte[] data, int bytesToSkip = 0, int indexToStart = 0)
        {
            this._data = data;
            this._bytesToSkip = bytesToSkip;
            this._indexToStart = indexToStart;
        }

        private ReadOnlyCollection<byte> Bytes => this.GetBytes(_data);

        public bool Equals(byte[] buffer) => this.GetBytes(buffer).SequenceEqual(Bytes);

        public IEnumerator<byte> GetEnumerator()
        {
            foreach (var magicByte in _data)
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

            var result = _indexToStart != 0
            ? this.TakeComparableSequence(bytes)
            : bytes.Skip(_bytesToSkip)
                    .Take(bytes.Length - _bytesToSkip)
                    .ToArray();

            return new ReadOnlyCollection<byte>(result);
        }

        private byte[] TakeComparableSequence(IReadOnlyCollection<byte> bytes)
        {
            var first = bytes.Take(_indexToStart)
                .ToList();

            var second = bytes.Skip(_bytesToSkip + first.Count)
                .Take(bytes.Count - (_bytesToSkip + first.Count));

            first.AddRange(second);
            return first.ToArray();
        }
    }
}
