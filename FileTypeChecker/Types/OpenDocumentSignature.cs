namespace FileTypeChecker.Types
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FileTypeChecker;

    /// <summary>
    /// Builds the magic-byte signature shared by the OpenDocument formats.
    /// </summary>
    /// <remarks>
    /// An OpenDocument file is a ZIP package whose first entry, per the ODF
    /// specification, is an uncompressed file named "mimetype" (with no extra field)
    /// whose content is the media-type string. The ZIP local file header is therefore:
    /// <list type="bullet">
    /// <item>offset 0: the <c>PK\x03\x04</c> local-header signature</item>
    /// <item>offsets 4-17: version, flags, compression method, modification timestamp
    /// and CRC-32 — values that vary between writers and from file to file</item>
    /// <item>offsets 18-25: compressed and uncompressed sizes, both equal to the
    /// (fixed) length of the media-type string because the entry is stored</item>
    /// <item>offsets 26-29: name length (8) and extra-field length (0)</item>
    /// <item>offset 30: the entry name "mimetype", immediately followed by the
    /// media-type string</item>
    /// </list>
    /// We match every byte that is invariant for a given format and skip only the
    /// variable region (offsets 4-17). The signature is one contiguous run from the PK
    /// prefix through the full media-type string, with the size fields placed early, so
    /// matching degrades gracefully with the amount of data available (the comparison
    /// runs only as far as the shorter of the signature and the buffer):
    /// <list type="bullet">
    /// <item>a full read (the library sizes its own buffer to the whole signature)
    /// compares the media-type string and so confirms the format by content</item>
    /// <item>a buffer truncated mid-string still matches on the bytes it does contain</item>
    /// <item>a buffer too short to reach the string falls back to the size and structure
    /// bytes, which is enough to keep the signature from collapsing to a bare ZIP prefix
    /// and tying with <c>ZipFile</c></item>
    /// </list>
    /// On a buffer too short to reach the string the size is only a weak heuristic, not a
    /// proof of OpenDocument content, but the only callers that hit that case are ones
    /// passing a deliberately tiny byte array — too little data to identify the subtype
    /// either way.
    /// </remarks>
    internal static class OpenDocumentSignature
    {
        private const int ZipPrefixLength = 4;
        private const int VariableRegionLength = 14; // offsets 4-17: version..CRC-32
        private const string MimeTypeEntryName = "mimetype";

        private static readonly byte[] ZipLocalFileHeaderPrefix = { 0x50, 0x4B, 0x03, 0x04 };
        private static readonly byte[] NameLengthAndNoExtraField = { 0x08, 0x00, 0x00, 0x00 };

        public static MagicSequence For(string mimeType)
        {
            // The stored entry's size equals the byte length of its content (the media-type
            // string), so derive both the size field and the matched bytes from the same
            // encoding to keep them consistent by construction.
            var mimeTypeBytes = Encoding.ASCII.GetBytes(mimeType);
            var storedSize = LittleEndian((uint)mimeTypeBytes.Length);

            var data = ZipLocalFileHeaderPrefix
                .Concat(new byte[VariableRegionLength])
                .Concat(storedSize)                                       // compressed size
                .Concat(storedSize)                                       // uncompressed size
                .Concat(NameLengthAndNoExtraField)
                .Concat(Encoding.ASCII.GetBytes(MimeTypeEntryName))
                .Concat(mimeTypeBytes)
                .ToArray();

            return new MagicSequence(data, bytesToSkip: VariableRegionLength, indexToStart: ZipPrefixLength);
        }

        private static IEnumerable<byte> LittleEndian(uint value) => new[]
        {
            (byte)value,
            (byte)(value >> 8),
            (byte)(value >> 16),
            (byte)(value >> 24)
        };
    }
}
