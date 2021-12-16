namespace FileTypeChecker
{
    using FileTypeChecker.Abstracts;

    public class MatchScore
    {
        public MatchScore(IFileType fileType, int score)
        {
            this.Type = fileType;
            this.Score = score;
        }

        public IFileType Type { get; set; }
        public int Score { get; set; }
    }
}
