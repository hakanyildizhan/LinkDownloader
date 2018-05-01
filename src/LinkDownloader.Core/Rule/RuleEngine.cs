using LinkDownloader.Core.Model;
using LinkDownloader.Core.Rule.Model;
using System.Collections.Generic;
using System.Linq;

namespace LinkDownloader.Core.Rule
{
	public class RuleEngine
	{
		private IList<IRule<Link>> _rules;
		private IList<Link> _links;

		public IList<Link> Links
		{
			get { return _links; }
			set { _links = value; }
		}

		public IList<LinkValidationResult> ValidationResults { get; set; }

		public RuleEngine(IList<Link> links)
		{
			Links = links;
			ValidationResults = new List<LinkValidationResult>();
			AddRules();
		}

		private void AddRules()
		{
			_rules = new List<IRule<Link>>();
			_rules.Add(new IncrementalRule());
			_rules.Add(new AccessibleRule());
			_rules = _rules.OrderByDescending(r => r.SortOrder).ToList();
		}

		public void RunRules()
		{
			IList<Link> validLinks = new List<Link>();
			IList<Link> processedPatterns = new List<Link>();
			
			foreach (Link link in Links)
			{
				foreach (IRule<Link> rule in _rules)
				{
					if (rule is IPreprocessingRule)
					{
						bool ruleResult = RunRule(link, rule);
						if (ruleResult)
						{
							processedPatterns.Add(link);
							foreach (Link processedLink in (rule as IPreprocessingRule).ProcessedLinkList)
							{
								validLinks.Add(processedLink);
							}
						}
					}
				}
			}

			processedPatterns.ToList().ForEach(pp => Links.Remove(pp));
			validLinks.ToList().ForEach(pl => Links.Add(pl));

			IList<Link> inaccessibleLinks = new List<Link>();

			foreach (Link link in Links)
			{
				foreach (IRule<Link> rule in _rules)
				{
					if (!(rule is IPreprocessingRule))
					{
						bool ruleResult = RunRule(link, rule);
						if (!ruleResult)
							inaccessibleLinks.Add(link);
					}
				}
			}

			inaccessibleLinks.ToList().ForEach(l => Links.Remove(l));

		}

		private bool RunRule(Link link, IRule<Link> rule)
		{
			bool linkPassesRule = rule.Run(link);

			ValidationResults.Add(new LinkValidationResult()
			{
				Link = link,
				IsValid = linkPassesRule,
				IsApplicable = rule.IsApplicable,
				ValidationMessage = !linkPassesRule && rule.IsApplicable ? rule.ValidationMessage : string.Empty
			});

			return linkPassesRule;
		}
	}
}
