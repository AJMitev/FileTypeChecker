using FileTypeChecker.Abstracts;

namespace FileTypeChecker
{
	public class MatchResult
	{
		public MatchResult(IFileType fileType)
		{
			this.Type = fileType;
		}

		public bool HasMatch => this.Type != null;
		public IFileType Type { get; }
	}
}