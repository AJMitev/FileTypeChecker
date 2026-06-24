namespace FileTypeChecker.Types
{
    using System;
    using System.IO;
    using Abstracts;
    using Common;
    using Exceptions;

    public class PortableDocumentFormat : FileType, IFileType
    {
        public const string TypeName = "Portable Document Format";
        public const string TypeMimeType = "application/pdf";
        public const string TypeExtension = "pdf";

        /// <summary>
        /// The PDF specification permits the "%PDF-" header to appear anywhere
        /// within the first 1024 bytes rather than strictly at offset 0 (for
        /// example when the document is wrapped in a MIME multipart envelope),
        /// so the header is searched for across that window instead of anchored
        /// to the start of the stream.
        /// </summary>
        private const int HeaderSearchWindow = 1024;

        private static readonly byte[] MagicBytes = { 0x25, 0x50, 0x44, 0x46, 0x2D };

        public PortableDocumentFormat() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }

        public override bool DoesMatchWith(Stream stream, bool resetPosition = true)
        {
            DataValidator.ThrowIfNull(stream, nameof(Stream));

            if (!stream.CanRead || (stream.Position != 0 && !stream.CanSeek))
                throw new StreamMustBeReadableException();

            if (stream.Position != 0 && resetPosition)
                stream.Position = 0;

            var buffer = new byte[HeaderSearchWindow];
            var read = FillBuffer(stream, buffer);

            return DoesMatchWith(buffer.AsSpan(0, read));
        }

        public override bool DoesMatchWith(byte[] bytes)
        {
            DataValidator.ThrowIfNull(bytes, nameof(Array));

            return DoesMatchWith((ReadOnlySpan<byte>)bytes);
        }

        public override bool DoesMatchWith(ReadOnlySpan<byte> bytes)
        {
            var windowLength = Math.Min(bytes.Length, HeaderSearchWindow);

            return bytes.Slice(0, windowLength).IndexOf(MagicBytes.AsSpan()) >= 0;
        }

        private static int FillBuffer(Stream stream, byte[] buffer)
        {
            var total = 0;
            int read;
            while (total < buffer.Length &&
                   (read = stream.Read(buffer, total, buffer.Length - total)) > 0)
            {
                total += read;
            }

            return total;
        }
    }
}
