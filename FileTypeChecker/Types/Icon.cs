namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Icon : FileType, IFileType
    {
        private const string name = "Icon File";
        private const string extension = FileExtension.Ico;
        private static readonly byte[] magicBytes = new byte[] { 0x00, 0x00, 0x01, 0x00};

        public Icon():base(name,extension,magicBytes)
        {

        }
    }
}
