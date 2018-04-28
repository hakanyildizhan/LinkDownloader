using LinkDownloader.Core;
using LinkDownloader.Core.Manager;
using LinkDownloader.Core.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace LinkDownloader.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			TextWriterTraceListener writer = new TextWriterTraceListener(System.Console.Out);
			Debug.Listeners.Add(writer);

			SetPreferences();

			IList<Link> linkList;

			System.Console.Write("Would you like the program to fetch links from the Links.xml file, or would you like to manually type them here? (Y/N) ");

			if (System.Console.ReadKey().Key == ConsoleKey.Y)
			{
				System.Console.WriteLine();
				System.Console.WriteLine("Type links to download below (separated by semicolon):");
				string input = System.Console.ReadLine();
				linkList = LinkHelper.ExtractLinksFromString(input);
			}
			else
			{
				System.Console.WriteLine();
				System.Console.WriteLine("Getting links from file.");
				linkList = XMLLinkReader.Instance.GetLinks();
			}

			if (linkList.Any())
			{
				System.Console.WriteLine("Starting jobs.");
				JobManager manager = new JobManager(linkList);
				manager.RunJobs().Wait();
			}
			
			else
				System.Console.WriteLine("No links found. Exiting.");

			System.Console.ReadLine();
		}

		private static void SetPreferences()
		{
			Preferences.DownloadPath = ConfigurationManager.AppSettings["SavePath"].ToString();
			Preferences.SimultaneousDownloads = Convert.ToInt32(ConfigurationManager.AppSettings["SimultaneousDownloads"].ToString());
			Preferences.ProgressReportInterval = Convert.ToInt32(ConfigurationManager.AppSettings["ProgressReportInterval"].ToString());
		}
	}
}
