using LinkDownloader.Core.Model;
using LinkDownloader.Core.Rule;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LinkDownloader.Core.Manager
{
	public class JobManager : IManager
	{
		private IList<Link> _links;
		public ConcurrentDictionary<int, IJob> Jobs { get; set; }

		public JobManager(IList<Link> links)
		{
			_links = links;
			Jobs = new ConcurrentDictionary<int, IJob>();
			RunRules();
			RegisterJobs();
		}

		public async Task RunJobs()
		{
			await ProcessQueuedJobs();
		}

		private void RegisterJobs()
		{
			for (int i = 0; i < _links.Count; i++)
			{
				IJob job = new Job();
				_links[i].Id = i + 1;
				job.Link = _links[i];
				job.Id = i + 1;
				Register(job);
			}
		}

		private void RunRules()
		{
			RuleEngine ruleEngine = new RuleEngine(_links);
			ruleEngine.RunRules();
			_links = new List<Link>();

			foreach (LinkValidationResult validationResult in ruleEngine.ValidationResults)
			{
				if (!validationResult.IsValid && validationResult.IsApplicable)
					WriteErrorToOutput(validationResult); 
			}

			_links = ruleEngine.Links;
		}

		private void WriteErrorToOutput(LinkValidationResult validationResult)
		{
			Debug.WriteLine($"Link {validationResult.Link.Title} is invalid: {validationResult.ValidationMessage}");
		}

		public void Register(IJob job)
		{
			job.JobCompleted += JobCompletedHandler;
			Jobs.TryAdd(job.Id, job);
		}

		public void Unregister(IJob job)
		{
			job.JobCompleted -= JobCompletedHandler;
		}

		private async Task ProcessQueuedJobs()
		{
			LimitedConcurrencyLevelTaskScheduler scheduler = new LimitedConcurrencyLevelTaskScheduler(Preferences.SimultaneousDownloads);
			TaskFactory factory = new TaskFactory(scheduler);

			Task t = await factory.StartNew(async () =>
			{
				for (int i = 0; i < Jobs.Count; i++)
				{
					Debug.WriteLine("Job with ID {0} queued", Jobs[i + 1].Id);
					await Jobs[i + 1].ExecuteAsync();
				}
			});
			t.Wait();
		}

		public void JobCompletedHandler(object sender, EventArgs e)
		{
			IJob job = (IJob)sender;
			Debug.WriteLine($"Job with ID {job.Id} completed.");
			Unregister(job);
		}
	}
}
