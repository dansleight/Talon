using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EagleRock.Bs
{
    /// <summary>
    /// This is from stack overflow
    /// </summary>
    public static class StringHelper
    {
        public static string SplitPascalCase(string pascalString)
        {
            return Regex.Replace(pascalString, @"(?<=[A-Za-z])(?=[A-Z][a-z])|(?<=[a-z0-9])(?=[0-9]?[A-Z])", " ");
		}

		public static string StripHtml(string toStrip)
		{
			if (string.IsNullOrEmpty(toStrip))
				return string.Empty;
			string toReturn = Regex.Replace(toStrip, @"<[^>]+>|&nbsp;", "").Trim();
			toReturn = Regex.Replace(toReturn, @"\s{2,}", " ");

			return toReturn;
		}
	}
}
