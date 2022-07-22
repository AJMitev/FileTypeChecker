namespace FileTypeChecker.Abstracts
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;

    public abstract class FileType : IFileType
    {
        private const string FileContentMustBeReadableErrorMessage = "File contents must be a readable stream";
        private const int ByfferDefaultSize = 20;
        private string name;
        private string extension;
        private byte[][] bytes;

        protected FileType(string name, string extension, byte[] magicBytes, int skipBytes = 0)
        {
            this.Name = name;
            this.Extension = extension;
            this.Bytes = new[] { magicBytes };
            SkipBytes = skipBytes;
        }

        protected FileType(string name, string extension, byte[][] magicBytesJaggedArray, int skipBytes = 0)
        {
            this.Name = name;
            this.Extension = extension;
            this.Bytes = magicBytesJaggedArray;
            SkipBytes = skipBytes;
        }
        
        public int SkipBytes { get; }

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

        private byte[][] Bytes
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
                throw new ArgumentException(FileContentMustBeReadableErrorMessage, nameof(Stream));
            }

            if (stream.Position != 0 && resetPosition)
            {
                stream.Position = 0;
            }

            var buffer = new byte[ByfferDefaultSize];
            stream.Read(buffer, 0, buffer.Length);
            return DoesMatchWith(buffer);
        }

        public async Task<bool> DoesMatchWithAsync(Stream stream, bool resetPosition = true)
        {
            DataValidator.ThrowIfNull(stream, nameof(Stream));

            if (!stream.CanRead || (stream.Position != 0 && !stream.CanSeek))
            {
                throw new ArgumentException(FileContentMustBeReadableErrorMessage, nameof(Stream));
            }

            if (stream.Position != 0 && resetPosition)
            {
                stream.Position = 0;
            }

            var buffer = new byte[ByfferDefaultSize];
            await stream.ReadAsync(buffer, 0, buffer.Length);
            return DoesMatchWith(buffer);
        }

        public int GetMatchingNumber(Stream stream)
        {
            stream.Position = 0;

            int counter = 0;
            var buffer = new byte[ByfferDefaultSize];
            stream.Read(buffer, 0, buffer.Length);

            foreach (var bytesArr in this.Bytes)
            {
                var shrinkedBuffer = buffer.Skip(SkipBytes).Take(bytesArr.Length);

                for (int i = counter; i < bytesArr.Length; i++)
                {
                    if (!bytesArr[i].Equals(buffer[i]))
                        break;

                    counter++;
                }
            }

            return counter == 0 ? -1 : counter;
        }

        private bool DoesMatchWith(byte[] buffer)
        {
            foreach (var bytesArr in this.Bytes)
            {
                if (buffer.Skip(SkipBytes).Take(bytesArr.Length).SequenceEqual(bytesArr)) 
                {
                    return true;
                }
            }
            return false;
        }
    }
}
