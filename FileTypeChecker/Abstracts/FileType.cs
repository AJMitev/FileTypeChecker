namespace FileTypeChecker.Abstracts
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using FileTypeChecker;
    using FileTypeChecker.Exceptions;

    public abstract class FileType : IFileType
    {
        private const int ByfferDefaultSize = 20;
        private string name;
        private string extension;
        private MagicSequence[] bytes;

        protected FileType(string name, string extension, byte[] magicBytes) : this(name, extension, new MagicSequence(magicBytes))
        {
        }

        protected FileType(string name, string extension, byte[][] magicBytes) : this(name, extension, magicBytes.Select(x => new MagicSequence(x)).ToArray())
        {
        }

        protected FileType(string name, string extension, MagicSequence magicBytes)
        {
            this.Name = name;
            this.Extension = extension;
            this.Bytes = new[] { magicBytes };
        }

        protected FileType(string name, string extension, MagicSequence[] magicBytesSequance)
        {
            this.Name = name;
            this.Extension = extension;
            this.Bytes = magicBytesSequance;
        }

        /// <inheritdoc />
        public string Name
        {
            get => this.name;

            private set
            {
                DataValidator.ThrowIfNullOrEmpty(value, nameof(Name));

                this.name = value;
            }
        }

        /// <inheritdoc />
        public string Extension
        {
            get => this.extension;
            private set
            {
                DataValidator.ThrowIfNullOrEmpty(value, nameof(Extension));

                this.extension = value;
            }
        }

        protected MagicSequence[] Bytes
        {
            get => this.bytes;
            set
            {
                DataValidator.ThrowIfNull(value, nameof(Bytes));

                this.bytes = value;
            }
        }

        /// <inheritdoc />
        public bool DoesMatchWith(Stream stream, bool resetPosition = true)
        {
            DataValidator.ThrowIfNull(stream, nameof(Stream));

            if (!stream.CanRead || (stream.Position != 0 && !stream.CanSeek))
            {
                throw new StreamMustBeReadableException();
            }

            if (stream.Position != 0 && resetPosition)
            {
                stream.Position = 0;
            }

            var buffer = new byte[ByfferDefaultSize];
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
            {
                throw new StreamMustBeReadableException();
            }

            if (stream.Position != 0 && resetPosition)
            {
                stream.Position = 0;
            }

            var buffer = new byte[ByfferDefaultSize];
            await stream.ReadAsync(buffer, 0, buffer.Length);

            foreach (var byteArray in this.Bytes)
            {
                if (byteArray.Equals(buffer))
                {
                    return true;
                }
            }

            return false;
        }

        public int GetMatchingNumber(Stream stream)
        {
            stream.Position = 0;

            int counter = 0;
            var buffer = new byte[ByfferDefaultSize];
            stream.Read(buffer, 0, buffer.Length);

            foreach (var bytesArr in this.Bytes)
            {
                var matchingBytes = bytesArr.CountMatchingBytes(buffer);
                if (matchingBytes > counter)
                {
                    counter = matchingBytes;
                }
            }

            return counter == 0 ? -1 : counter;
        }

        public int GetMatchingNumber(byte[] bytes)
        {
            int counter = 0;
        
            foreach (var bytesArr in this.Bytes)
            {
                var matchingBytes = bytesArr.CountMatchingBytes(bytes);
                if (matchingBytes > counter)
                {
                    counter = matchingBytes;
                }
            }

            return counter == 0 ? -1 : counter;
        }

        private bool CompareBytes(byte[] bytes)
        {
            foreach (var byteArray in this.Bytes)
            {
                if (byteArray.Equals(bytes))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
