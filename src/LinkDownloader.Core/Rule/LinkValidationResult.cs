using LinkDownloader.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDownloader.Core.Rule
{
	public class LinkValidationResult
	{
		public Link Link { get; set; }
		public bool IsValid { get; set; }
		public bool IsApplicable { get; set; } = true;
		public string ValidationMessage { get; set; }
	}
}
