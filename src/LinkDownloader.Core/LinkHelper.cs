using LinkDownloader.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDownloader.Core
{
	public static class LinkHelper
	{
		public static string ExtractFileName(string url)
		{
			return url.Split('/').ToList().LastOrDefault();
		}

		public static List<Link> ExtractLinksFromString(string source, char separator = ';')
		{
			List<Link> links = new List<Link>();
			source.Split(separator).ToList().ForEach(l => links.Add(new Link { Title = LinkHelper.ExtractFileName(l), Url = l }));
			return links;
		}
	}
}
