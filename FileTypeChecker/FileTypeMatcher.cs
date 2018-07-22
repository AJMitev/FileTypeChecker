namespace FileTypeChecker
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public  class FileTypeMatcher : IFileTypeMatcher
    {
        private readonly byte[] _bytes;

        public FileTypeMatcher(IEnumerable<byte> bytes)
        {
            this._bytes =bytes.ToArray();
        }

        protected bool MatchesPrivate(Stream stream)
        {
            foreach (var b in this._bytes)
            {
                if (stream.ReadByte() != b)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Matches(Stream stream, bool resetPosition = true)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (!stream.CanRead || (stream.Position != 0 && !stream.CanSeek))
            {
                throw new ArgumentException("File contents must be a readable stream", "stream");
            }
            if (stream.Position != 0 && resetPosition)
            {
                stream.Position = 0;
            }

            return MatchesPrivate(stream);
        }

    }
}
