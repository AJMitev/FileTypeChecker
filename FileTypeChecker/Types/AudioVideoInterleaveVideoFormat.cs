﻿namespace FileTypeChecker.Types
{
    using Abstracts;

    public class AudioVideoInterleaveVideoFormat : FileType, IFileType
    {
        public const string TypeName = "Audio Video Interleave video format";
        public const string TypeExtension = "avi";
        private static readonly byte[] MagicBytes = { 0x52, 0x49, 0x46, 0x46 };

        public AudioVideoInterleaveVideoFormat() : base(TypeName, TypeExtension, MagicBytes)
        {
        }
    }
}
