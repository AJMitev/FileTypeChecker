using FileTypeChecker.Abstracts;

namespace FileTypeChecker.Types
{
    public class Mp4 : FileType, IFileType
    {
        private const string name = "MP4 file";
        private const string extension = FileExtension.Mp4;
        private static readonly byte[][] magicBytesJaggedArray = { new byte[] { 0x66, 0x74 , 0x79 , 0x70 , 0x4D , 0x53 , 0x4E , 0x56 }, 
            new byte[] { 0x66, 0x74 , 0x79 , 0x70 , 0x69 , 0x73 , 0x6F , 0x6D }};

        public Mp4() : base(name, extension, magicBytesJaggedArray, 4)
        {
        }
    }
}