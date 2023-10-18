namespace FileTypeChecker
{
    using Abstracts;

    public class MatchScore
    {
        public MatchScore(IFileType fileType, int score)
        {
            this.Type = fileType;
            this.Score = score;
        }

        public IFileType Type { get; }
        public int Score { get; }
    }
}
