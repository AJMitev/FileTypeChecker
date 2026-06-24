using FileTypeChecker.Abstracts;

namespace FileTypeChecker.Types
{
    /// <summary>
    /// MPEG-4 audio file (AAC). Identified by ftyp box with M4A or M4B brand.
    /// </summary>
    public class M4a : FileType, IFileType
    {
        public const string TypeName = "MPEG-4 audio file";
        public const string TypeMimeType = "audio/mp4";
        public const string TypeExtension = "m4a";
        private static readonly MagicSequence[] MagicBytesSequence =
        {
            new(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x41, 0x20 }, 4), // ftypM4A
            new(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x42, 0x20 }, 4), // ftypM4B
        };

        public M4a() : base(TypeName, TypeMimeType, TypeExtension, MagicBytesSequence)
        {
        }
    }
}
