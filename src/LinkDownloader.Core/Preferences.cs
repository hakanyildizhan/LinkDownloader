using System;
using System.IO;

namespace LinkDownloader.Core
{
	public static class Preferences
	{
		private static string _downloadPath = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal)), "Downloads");
		private static int _progressReportInterval = 10;

		public static string DownloadPath
		{
			get { return _downloadPath; }
			set
			{
				if (Directory.Exists(value))
					_downloadPath = value;
			}
		}

		public static int ProgressReportInterval
		{
			get { return _progressReportInterval; }
			set
			{
				if (value >= 1 && value <= 100)
					_progressReportInterval = value;
			}
		}

		public static int SimultaneousDownloads { get; set; } = 1;

	}
}
