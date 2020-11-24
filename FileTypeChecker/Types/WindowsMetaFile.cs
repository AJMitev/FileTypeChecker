namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WindowsMetaFileType : FileType, IFileType 
    {
        private const string name = "Windows Meta File";
        private const string extension = FileExtension.Wmf;
        private static readonly byte[] magicBytes = new byte[] { 0xD7, 0xCD, 0xC6, 0x9A };

        public WindowsMetaFileType() : base(name, extension, magicBytes)
        { }
    }
}
