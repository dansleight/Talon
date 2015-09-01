using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs
{
    public static class BsHelper
    {
        public static string SplitCamelCase(string input)
        {
            string toReturn = "";
            foreach (string part in System.Text.RegularExpressions.Regex.Split(input, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})"))
            {
                toReturn += part + " ";
            }
            return toReturn.Trim();
        }

        public static void SetLabelSize(int labelSize)
        {
            System.Web.HttpContext.Current.Items["bslabel-size"] = labelSize;
        }

        public static int GetLabelSize()
        {
            if (System.Web.HttpContext.Current.Items["bslabel-size"] != null)
            {
                int labelSize;
                if (Int32.TryParse(System.Web.HttpContext.Current.Items["bslabel-size"].ToString(), out labelSize))
                {
                    return labelSize;
                }
            }
            // the default is 2
            return 2;
        }
    }
}
