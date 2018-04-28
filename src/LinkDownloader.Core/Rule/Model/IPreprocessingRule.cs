using LinkDownloader.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDownloader.Core.Rule.Model
{
	public interface IPreprocessingRule : IRule<Link>
	{
		/// <summary>
		/// Links that are generated from a pattern by way of a preprocessing rule
		/// </summary>
		IList<Link> ProcessedLinkList { get; set; }
	}
}
