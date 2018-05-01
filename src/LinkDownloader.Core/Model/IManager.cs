using System;

namespace LinkDownloader.Core.Model
{
	public interface IManager
	{
		void Register(IJob job);
		void Unregister(IJob job);
		void JobCompletedHandler(object sender, EventArgs e);
	}
}
