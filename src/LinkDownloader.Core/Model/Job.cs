using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkDownloader.Core.Model
{
	public class Job : IJob
	{
		public int Id { get; set; } = 0;
		public Link Link { get; set; }
		public JobStatus Status { get; set; } = JobStatus.NotStarted;
		public int PercentageCompleted { get; set; } = 0;
		public long ContentLength { get; set; }
		private bool _progressIntervalReached = false;

		public event EventHandler JobCompleted;

		public async Task ExecuteAsync()
		{
			using (WebClient wc = new WebClient())
			{
				wc.DownloadProgressChanged += wc_DownloadProgressChanged;

				HttpWebRequest request = WebRequest.Create(Link.Url) as HttpWebRequest;
				request.Method = "HEAD";
				try
				{
					using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
					{
						ContentLength = response.ContentLength;
					}
				}
				catch (Exception)
				{
					// contentlength could not be retrieved
				}

				Status = JobStatus.Started;

				try
				{
					await wc.DownloadFileTaskAsync(new Uri(Link.Url), Path.Combine(Preferences.DownloadPath, LinkHelper.ExtractFileName(Link.Url)));
				}
				catch (Exception)
				{
					Wc_DownloadFileFailed();
				}
			}
		}

		private void Wc_DownloadFileFailed()
		{
			Status = JobStatus.Failed;
			NotifyJobCompleted();
		}

		private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			Status = JobStatus.Completed;
			NotifyJobCompleted();
		}

		private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PercentageCompleted = e.ProgressPercentage;
			if (e.BytesReceived == e.TotalBytesToReceive || PercentageCompleted.Equals(100))
			{
				Status = JobStatus.Completed;
				NotifyJobCompleted();
			}
			
			else if (!_progressIntervalReached && PercentageCompleted % Preferences.ProgressReportInterval == 0 && PercentageCompleted != 0)
			{
				_progressIntervalReached = true;
				Debug.WriteLine($"Job with ID {Id}: {PercentageCompleted}% completed");
			}
			else if (PercentageCompleted % Preferences.ProgressReportInterval > 0)
			{
				_progressIntervalReached = false;
			}
		}

		public void NotifyJobCompleted()
		{
			JobCompleted?.Invoke(this, EventArgs.Empty);
		}
	}

	public enum JobStatus
	{
		NotStarted,
		Started,
		Completed,
		Failed
	}
}
