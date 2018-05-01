using System;
using System.Threading.Tasks;

namespace LinkDownloader.Core.Model
{
	public interface IJob
	{
		int Id { get; set; }
		JobStatus Status { get; set; }
		Link Link { get; set; }
		int PercentageCompleted { get; set; }
		event EventHandler JobCompleted;
		void NotifyJobCompleted();
		Task ExecuteAsync();
	}
}
