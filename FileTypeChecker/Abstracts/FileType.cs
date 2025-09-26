namespace FileTypeChecker.Abstracts
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using FileTypeChecker;
    using Exceptions;

    public abstract class FileType : IFileType
    {
        private int BufferSize => Math.Max(MaxMagicSequenceLength, 20);
        private string _name;
        private string _mimeType;
        private string _extension;
        private MagicSequence[] _bytes;

        protected FileType(string name, string mimeType, string extension, byte[] magicBytes) : this(name, mimeType, 
            extension, new MagicSequence(magicBytes))
        {
        }

        protected FileType(string name, string mimeType, string extension, byte[][] magicBytes) : this(name, mimeType, 
            extension, magicBytes.Select(x => new MagicSequence(x)).ToArray())
        {
        }

        protected FileType(string name, string mimeType, string extension, MagicSequence magicBytes)
        {
            this.Name = name;
            this.MimeType = mimeType;
            this.Extension = extension;
            this.Bytes = new[] { magicBytes };
        }

        protected FileType(string name, string mimeType, string extension, MagicSequence[] magicBytesSequence)
        {
            this.Name = name;
            this.MimeType =  mimeType;
            this.Extension = extension;
            this.Bytes = magicBytesSequence;
        }

        /// <inheritdoc />
        public string Name
        {
            get => this._name;

            private set
            {
                DataValidator.ThrowIfNullOrEmpty(value, nameof(Name));

                this._name = value;
            }
        }

        /// <inheritdoc />
        public string Extension
        {
            get => this._extension;
            private set
            {
                DataValidator.ThrowIfNullOrEmpty(value, nameof(Extension));

                this._extension = value;
            }
        }

        public string MimeType
        {
            get => this._mimeType;
            private set
            {
                DataValidator.ThrowIfNullOrEmpty(value, nameof(MimeType));
                
                this._mimeType = value;
            }
        }

        protected MagicSequence[] Bytes
        {
            get => this._bytes;
            set
            {
                DataValidator.ThrowIfNull(value, nameof(Bytes));

                this._bytes = value;
            }
        }

        /// <inheritdoc />
        public int MaxMagicSequenceLength => Bytes.Max(o => o.Length);

        /// <inheritdoc />
        public bool DoesMatchWith(Stream stream, bool resetPosition = true)
        {
            DataValidator.ThrowIfNull(stream, nameof(Stream));

            if (!stream.CanRead || (stream.Position != 0 && !stream.CanSeek))
                throw new StreamMustBeReadableException();

            if (stream.Position != 0 && resetPosition)
                stream.Position = 0;

            var buffer = new byte[BufferSize];
            stream.Read(buffer, 0, buffer.Length);

            return CompareBytes(buffer);
        }

        public bool DoesMatchWith(byte[] bytes)
        {
            DataValidator.ThrowIfNull(bytes, nameof(Array));

            return CompareBytes(bytes);
        }

        public async Task<bool> DoesMatchWithAsync(Stream stream, bool resetPosition = true)
        {
            DataValidator.ThrowIfNull(stream, nameof(Stream));

            if (!stream.CanRead || (stream.Position != 0 && !stream.CanSeek))
                throw new StreamMustBeReadableException();

            if (stream.Position != 0 && resetPosition)
                stream.Position = 0;

            var buffer = new byte[BufferSize];
            await stream.ReadAsync(buffer, 0, buffer.Length);

            return CompareBytes(buffer);
        }

        public int GetMatchingNumber(Stream stream)
        {
            var buffer = new byte[BufferSize];
            stream.Position = 0;
            stream.Read(buffer, 0, buffer.Length);

            return GetMatchingNumber(buffer);
        }

        public int GetMatchingNumber(byte[] bytes)
        {
            var counter = this.Bytes
                .Select(bytesArr => bytesArr.CountMatchingBytes(bytes))
                .Prepend(0)
                .Max();

            return counter == 0 ? -1 : counter;
        }

        private bool CompareBytes(byte[] bytes) => this.Bytes.Any(byteArray => byteArray.Equals(bytes));

        // High-performance ReadOnlySpan<byte> overloads

        /// <inheritdoc />
        public bool DoesMatchWith(ReadOnlySpan<byte> bytes) => CompareBytes(bytes);

        /// <inheritdoc />
        public int GetMatchingNumber(ReadOnlySpan<byte> bytes) 
        {
            var matches = 0;
            foreach (var magicSequence in this.Bytes)
            {
                matches += magicSequence.CountMatchingBytes(bytes);
            }

            return matches;
        }

        private bool CompareBytes(ReadOnlySpan<byte> bytes) 
        {
            foreach (var byteArray in this.Bytes)
            {
                if (byteArray.Equals(bytes))
                    return true;
            }
            return false;
        }
    }
}