namespace FileTypeChecker.Types
{
    using FileTypeChecker.Abstracts;

    public class JointPhotographicExpertsGroup : FileType, IFileType
    {
        private static readonly string name = "JPEG";
        private static readonly string extension = "jpg";
        private static readonly byte[] magicBytes = new byte[] { 0xFF, 0xD8, 0xFF };

        public JointPhotographicExpertsGroup() : base(name, extension, magicBytes)
        {
        }
    }
}
