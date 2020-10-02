namespace FileTypeChecker.Abstracts
{
    using System;
    using System.IO;
    using System.Linq;
    using Common;

    public abstract class FileType : IFileType
    {
        private const string FileContentMustBeReadableErrorMessage = "File contents must be a readable stream";

        private string name;
        private string extension;
        private byte[][] bytes;

        public FileType(string name, string extension, byte[] magicBytes, int skipBytes = 0)
        {
            this.Name = name;
            this.Extension = extension;
            this.Bytes = new[] { magicBytes };
            SkipBytes = skipBytes;
        }

        public FileType(string name, string extension, byte[][] magicBytesJaggedArray, int skipBytes = 0)
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

            var buffer = new byte[20];
            stream.Read(buffer, 0, buffer.Length);
            return CompareBytes(buffer);
        }
        
        public bool DoesMatchWith(byte[] buffer)
        {
            return CompareBytes(buffer);
        }


        protected bool CompareBytes(byte[] buffer)
        {
            foreach (var x in this.Bytes)
            {
                if (buffer.Skip(SkipBytes).Take(x.Length).SequenceEqual(x)) 
                {
                    return true;
                }
            }
            return false;
        }
    }
}
