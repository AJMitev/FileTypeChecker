namespace FileTypeChecker
{
    using FileTypeChecker.Abstracts;

    internal class MatchScore
    {
        internal MatchScore(IFileType fileType, int score)
        {
            this.Type = fileType;
            this.Score = score;
        }

        public IFileType Type { get; set; }
        public int Score { get; set; }
    }
}
