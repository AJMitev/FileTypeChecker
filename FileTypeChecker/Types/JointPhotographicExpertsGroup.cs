namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class JointPhotographicExpertsGroup : FileType, IFileType
    {
        private const string name = "JPEG";
        private const string extension = FileExtension.Jpg;
        private static readonly byte[] magicBytes = new byte[] { 0xFF, 0xD8, 0xFF };

        public JointPhotographicExpertsGroup() : base(name, extension, magicBytes)
        {
        }
    }
}
