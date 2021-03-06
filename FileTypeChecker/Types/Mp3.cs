﻿namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;
    public class Mp3 : FileType, IFileType
    {
        private const string name = "MPEG-1 Audio Layer 3 (MP3) audio file";
        private const string extension = FileExtension.Mp3;
        private static readonly byte[][] magicBytesJaggedArray = {new byte[] { 0x49, 0x44, 0x33 } };

        public Mp3() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
