using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Mvc
{
    public static class Serialize
    {
        private static object GetPropValue(object src, string propName) {
            try {
                return src.GetType().GetProperty(propName).GetValue(src, null);
            }
            catch {
                return null;
            }
            
        }

        public static List<string> ObjectToStringList(object src, List<string> properyNames)
        {
            List<string> toReturn = new List<string>();
            foreach (string propName in properyNames)
            {
                toReturn.Add(GetPropValue(src, propName).ToString());
            }
            return toReturn;
        }

        public static List<List<string>> ObjectsToLists(IEnumerable<object> srcs, List<string> properyNames)
        {
            List<List<string>> toReturn = new List<List<string>>();
            foreach (object src in srcs)
            {
                toReturn.Add(ObjectToStringList(src, properyNames));
            }
            return toReturn;
        }
    }
}