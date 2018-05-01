using System;
using System.Linq;

namespace LinkDownloader.Core
{
	public static class NumberHelper
	{
		public static int GetPrecedingZeroes(string rangeExpression)
		{
			uint rangeStart = Convert.ToUInt32(rangeExpression);
			int leadingZeroes = rangeExpression.Length != rangeStart.ToString().Length ? rangeExpression.Substring(0, rangeExpression.LastIndexOf(rangeStart.ToString())).Count(c => c == '0') : 0;
			return leadingZeroes;
		}
	}
}
