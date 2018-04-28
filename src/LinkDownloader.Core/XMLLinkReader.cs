using LinkDownloader.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LinkDownloader.Core
{
	public class XMLLinkReader
	{
		private static readonly XMLLinkReader instance = new XMLLinkReader();
		private XmlDocument _doc;

		public static XMLLinkReader Instance
		{
			get { return instance; }
		}

		private XMLLinkReader()
		{
			_doc = new XmlDocument();
			_doc.Load(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase), "Links.xml"));
		}

		public IList<Link> GetLinks()
		{
			IList<Link> links = new List<Link>();
			XmlNodeList linkList = _doc.GetElementsByTagName("Link");
			for (int i = 0; i < linkList.Count; i++)
			{
				string title = linkList[i].Attributes["title"].Value;
				string url = linkList[i].Attributes["url"].Value;
				links.Add(new Link
				{
					Title = title,
					Url = url
				});
			}
			return links;
		}
	}
}
