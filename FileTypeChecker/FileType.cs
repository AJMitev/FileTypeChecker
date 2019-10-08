namespace FileTypeChecker
{
    using System;
    using System.IO;
    using System.Linq;
    using Abstracts;
    using Common;

    internal class FileType : IFileType
    {
        private const string FileContentMustBeReadableErrorMessage = "File contents must be a readable stream";

        private string name;
        private string extension;
        private byte[] bytes;

        internal FileType(string name, string extension, byte[] magicBytes)
        {
            this.Name = name;
            this.Extension = extension;
            this.Bytes = magicBytes;
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


        private byte[] Bytes
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

            return CompareBytes(stream);
        }


        protected bool CompareBytes(Stream stream)
        {
            return this.Bytes.All(b => stream.ReadByte() == b);
        }
    }
}
