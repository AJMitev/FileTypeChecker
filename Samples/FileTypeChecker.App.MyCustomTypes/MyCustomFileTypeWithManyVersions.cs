namespace FileTypeChecker.App.MyCustomTypes
{
    using FileTypeChecker.Abstracts;

    class MyCustomFileTypeWithManyVersions : FileType
    {
        private static readonly string name = "My Super Cool Custom Type 1.0";
        private static readonly string extension = "ext2";
        private static readonly byte[][] magicBytesJaggedArray = { new byte[] { 0xAF }, new byte[] { 0xEF } };

      
        public MyCustomFileTypeWithManyVersions() : base(name, extension, magicBytesJaggedArray)
        {
        }
    }
}
