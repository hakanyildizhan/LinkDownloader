using LinkDownloader.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkDownloader.Core.Rule.Model
{
	public class IncrementalRule : IPreprocessingRule
	{
		public int RuleId { get; set; } = 1;
		public string RuleName { get; set; } = "Incremental Link Rule";
		public string ValidationMessage { get; set; } = "Invalid incremental link pattern!";
		public bool IsEnabled { get; set; } = true;
		public int SortOrder { get; set; } = 100;
		public IList<Link> ProcessedLinkList { get; set; }
		public bool IsApplicable { get; set; } = true;

		public IncrementalRule()
		{
			ProcessedLinkList = new List<Link>();
		}

		public bool Run(Link link)
		{
			bool isValid = false;

			if (!link.Url.Contains('*'))
				IsApplicable = false;

			else // this is an incremental link. Check for increment setting
			{
				Match match = Regex.Match(link.Url, @"\[(\d+)-(\d+)\]$");
				if (match.Success)
				{
					string rangeStartExpression = match.Groups[1].Value;
					string rangeEndExpression = match.Groups[2].Value;

					if (Convert.ToUInt16(rangeStartExpression) < Convert.ToUInt16(rangeEndExpression))
					{
						isValid = true;
						GenerateLinks(link, rangeStartExpression, rangeEndExpression);
					}
				}
			}
			
			return isValid;
		}

		private void GenerateLinks(Link link, string rangeStartExpression, string rangeEndExpression)
		{
			for (int i = Convert.ToUInt16(rangeStartExpression); i < Convert.ToUInt16(rangeEndExpression) + 1; i++)
			{
				Match match = Regex.Match(link.Url, @"(\s+\[\d+-\d+\])$");
				if (match.Success)
				{
					string url = link.Url.Replace(match.Groups[1].Value, string.Empty);
					int precedingZeroes = rangeStartExpression.Count(c => c == '0');
					url = url.Replace("*", i.ToString().PadLeft(precedingZeroes+1, '0'));
					string linkTitle = link.Title + i.ToString();
					ProcessedLinkList.Add(new Link
					{
						Title = linkTitle,
						Url = url
					});
				}
			}
		}
	}
}
