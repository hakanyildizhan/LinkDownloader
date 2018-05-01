using LinkDownloader.Core.Model;
using System;
using System.Net;

namespace LinkDownloader.Core.Rule.Model
{
	public class AccessibleRule : IRule<Link>
	{
		public int RuleId { get; set; } = 2;
		public string RuleName { get; set; } = "Link Accessibility Rule";
		public string ValidationMessage { get; set; } = "Broken link!";
		public bool IsEnabled { get; set; } = true;
		public int SortOrder { get; set; } = 500;
		public bool IsApplicable { get; set; } = true;

		public bool Run(Link link)
		{
			bool isValid = false;

			try
			{
				HttpWebRequest request = WebRequest.Create(link.Url) as HttpWebRequest;
				request.Method = "HEAD";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					isValid = response.StatusCode == HttpStatusCode.OK;
				}
			}
			catch (Exception)
			{
				// link is not accessible
			}

			return isValid;
		}
	}
}
