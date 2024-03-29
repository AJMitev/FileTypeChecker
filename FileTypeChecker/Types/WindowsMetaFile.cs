﻿namespace FileTypeChecker.Types
{
    using Abstracts;

    public class WindowsMetaFileType : FileType, IFileType 
    {
        public const string TypeName = "Windows Meta File";
        public const string TypeExtension = "wmf";
        private static readonly byte[] MagicBytes = { 0xD7, 0xCD, 0xC6, 0x9A };

        public WindowsMetaFileType() : base(TypeName, TypeExtension, MagicBytes)
        { }
    }
}
