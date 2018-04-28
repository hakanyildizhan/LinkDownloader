using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDownloader.Core.Model
{
	public interface IManager
	{
		void Register(IJob job);
		void Unregister(IJob job);
		void JobCompletedHandler(object sender, EventArgs e);
	}
}
