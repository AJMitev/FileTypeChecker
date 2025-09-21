namespace FileTypeChecker.Types
{
    using Abstracts;

    public class JointPhotographicExpertsGroup : FileType, IFileType
    {
        public const string TypeName = "JPEG";
        public const string TypeMimeType = "image/jpeg";
        public const string TypeExtension = "jpg";
        private static readonly byte[] MagicBytes = { 0xFF, 0xD8, 0xFF };

        public JointPhotographicExpertsGroup() : base(TypeName, TypeMimeType, TypeExtension, MagicBytes)
        {
        }
    }
}
