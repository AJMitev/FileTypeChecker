namespace FileTypeChecker.App.MyCustomTypes
{
    using FileTypeChecker;
    using Abstracts;

    class MyCustomFileTypeWithManyVersions : FileType
    {
        private const string name = "My Super Cool Custom Type 1.0";
        private const string mimeType = "application/x-my-super-cool-custom-typev1";
        private const string extension = "ext2";
        private static readonly MagicSequence[] magicBytesJaggedArray = { new(new byte[] { 0xAF }), new(new byte[] { 0xEF }) };

        public MyCustomFileTypeWithManyVersions() : base(name, mimeType, extension, magicBytesJaggedArray) { }
    }
}
