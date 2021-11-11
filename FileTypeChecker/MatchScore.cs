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

        internal IFileType Type { get; set; }
        internal int Score { get; set; }
    }
}
