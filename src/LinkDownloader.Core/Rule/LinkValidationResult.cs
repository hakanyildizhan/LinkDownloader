using LinkDownloader.Core.Model;

namespace LinkDownloader.Core.Rule
{
	public class LinkValidationResult
	{
		public Link Link { get; set; }
		public bool IsValid { get; set; }
		public bool IsApplicable { get; set; } = true;
		public string ValidationMessage { get; set; }
	}
}
