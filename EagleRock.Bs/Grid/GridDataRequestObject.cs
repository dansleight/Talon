using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace EagleRock.Bs.Grid
{
    public class GridDataRequestObject<T> where T : class
    {
        #region DataTables.net passed arguments

        public List<GridDataRequestColumn> columns { get; set; }

        public List<GridDataRequestOrder> order { get; set; }

        public GridDataRequestSearch search { get; set; }

        public int start { get; set; }
        public int length { get; set; }
        public int draw { get; set; }

        #endregion

        #region Static Methods

        private static string getPropertyValue(object src, GridDataRequestColumn c)
        {
            // this really exists with only minor differences in two places, and we should consider a way to merge them into one
            string propName = c.data;
            try
            {
                if (!String.IsNullOrWhiteSpace(c.template))
                {
                    // this is a custom column that needs processed
                    string toReturn = HttpUtility.HtmlDecode(c.template);
                    var regex = new Regex("{.*?}");
                    var toProcess = regex.Matches(c.template);
                    foreach (var match in toProcess)
                    {
                        try
                        {
                            string matching = match.ToString();
                            string col = matching.Trim(new char[] { '{', '}' });
                            string val = src.GetType().GetProperty(col).GetValue(src, null).ToString();
                            toReturn = toReturn.Replace(matching, val);
                        }
                        catch
                        { }
                    }
                    return toReturn;
                }
                else
                {
                    Type t = src.GetType().GetProperty(propName).PropertyType;
                    if (t == typeof(DateTime) && !string.IsNullOrWhiteSpace(c.format))
                    {
                        return ((DateTime)src.GetType().GetProperty(propName).GetValue(src, null)).ToString(c.format);
                    }
                    return src.GetType().GetProperty(propName).GetValue(src, null).ToString();
                }
            }
            catch { }
            return string.Empty;
        }

        #endregion

        #region private Object Methods

        private List<gridProcessedObject<T>> processDataset(List<T> dataset)
        {
            var r = new List<gridProcessedObject<T>>();
            foreach (var o in dataset)
            {
                Dictionary<string, string> a = new Dictionary<string, string>();
                string searchable = string.Empty;
                foreach (GridDataRequestColumn c in columns)
                {
                    string colVal = getPropertyValue(o, c);
                    a.Add(c.data, colVal);
                    if (c.searchable)
                    {
                        if (c.isCustom())
                        {
                            string collapsed = Regex.Replace(Regex.Replace(colVal, "<[^>]*>", " "), "\\s+", " ").Trim();
                            searchable += collapsed + " ";
                        }
                        else
                            searchable += colVal + " ";
                    }
                }
                r.Add(new gridProcessedObject<T>() { original = o, processed = a, searchable = searchable });
            }
            return r;
        }

        private List<gridProcessedObject<T>> processSearch(List<gridProcessedObject<T>> processedDataset)
        {
            if (!String.IsNullOrEmpty(search.value))
            {
                List<string> searchTerms = search.value.Split(new char[] { ',', ' ', ':', ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (string searchTerm in searchTerms)
                {
                    processedDataset = processedDataset.Where(x => x.searchable.ToUpper().Contains(searchTerm.ToUpper())).ToList();
                }
            }
            return processedDataset;
        }

        private List<gridProcessedObject<T>> processSort(List<gridProcessedObject<T>> processedDataset)
        {
            if (order == null || order.Count == 0)
                return processedDataset;
            List<GridDataRequestOrder> toProcess = new List<GridDataRequestOrder>();
            toProcess.AddRange(order);
            toProcess.Reverse();
            foreach (var o in toProcess)
            {
                var column = columns[int.Parse(o.column)];
                if (column.isCustom())
                {
                    processedDataset.ForEach(x =>
                    {
                        string processedVal = ((IDictionary<string, string>)x.processed)[column.data];
                        //string stripped = Regex.Replace(processedVal, "<[^>]*>", " ");
                        //string collapsed = Regex.Replace(stripped, "\\s+", " ").Trim();
                        //x.stringsortval = collapsed;
                        x.stringsortval = processedVal;
                        if (o.dir == "asc")
                            processedDataset = processedDataset.OrderBy(p => p.stringsortval).ToList();
                        else
                            processedDataset = processedDataset.OrderByDescending(p => p.stringsortval).ToList();
                    });
                }
                else
                {
                    string columnName = column.data;
                    Type cType = typeof(T).GetProperty(columnName).PropertyType;
                    processedDataset.ForEach(x =>
                    {
                        x.sortval = x.original.GetType().GetProperty(columnName).GetValue(x.original, null).ToString();
                    });
                    if (o.dir == "asc")
                        processedDataset = processedDataset.OrderBy(p => p.sortval, new GenericTypeComparer(cType)).ToList();
                    else
                        processedDataset = processedDataset.OrderByDescending(p => p.sortval, new GenericTypeComparer(cType)).ToList();
                }

            }
            return processedDataset;
        }

        #endregion

        #region Public Object Methods

        public GridDataResult GetResult(List<T> dataset)
        {
            return GetResultAndCache(dataset, null, 0);
        }

        public GridDataResult GetResultAndCache(List<T> dataset, string cacheKey, int cacheForSeconds)
        {
            List<gridProcessedObject<T>> processedDataset = processDataset(dataset);

            if (!String.IsNullOrEmpty(cacheKey) && cacheForSeconds > 0)
            {
                System.Web.HttpContext.Current.Cache.Add(cacheKey,
                    processedDataset,
                    null,
                    DateTime.Now.AddSeconds(cacheForSeconds),
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.High,
                    null);
            }

            processedDataset = processSearch(processedDataset);
            processedDataset = processSort(processedDataset);

            GridDataResult res = new GridDataResult();
            if (length > 0)
                res.data = processedDataset.Select(x => x.processed).Skip(start).Take(length).ToList();
            else
                res.data = processedDataset.Select(x => x.processed).ToList();
            res.draw = draw;
            res.recordsTotal = dataset.Count;
            res.recordsFiltered = processedDataset.Count;
            return res;
        }

        List<gridProcessedObject<T>> cachedProcessedDataset;
        public bool HasCache(string cacheKey)
        {
            if(System.Web.HttpContext.Current.Cache[cacheKey] != null)
            {
                cachedProcessedDataset = (List<gridProcessedObject<T>>)System.Web.HttpContext.Current.Cache[cacheKey];
                return true;
            }
            return false;
        }

        public GridDataResult GetResultFromCache()
        {
            if (cachedProcessedDataset == null)
                throw new Exception("No cached dataset has been loaded via \"HasCache(cacheKey)\"");
            GridDataResult res = new GridDataResult();
            res.recordsTotal = cachedProcessedDataset.Count;
            List<gridProcessedObject<T>> processedDataset = cachedProcessedDataset;

            processedDataset = processSearch(processedDataset);
            processedDataset = processSort(processedDataset);

            res.data = processedDataset.Select(x => x.processed).Skip(start).Take(length).ToList();

            res.draw = draw;
            res.recordsFiltered = processedDataset.Count;
            res.fromcache = true;
            return res;
        }

        #endregion
    }

    public class GridDataRequestColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public string format { get; set; }
        public string template { get; set; }

        internal bool isCustom()
        {
            return !string.IsNullOrWhiteSpace(template);
        }
    }

    public class GridDataRequestOrder
    {
        public string column { get; set; }
        public string dir { get; set; }
    }

    public class GridDataRequestSearch
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

    public class GridDataResult
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<dynamic> data { get; set; }
        public string error { get; set; }
        public bool fromcache { get; set; }
    }

    class gridProcessedObject<T>
    {
        public T original { get; set; }
        public dynamic processed { get; set; }
        public string searchable { get; set; }
        internal string sortval { get; set; }
        internal string stringsortval { get; set; }
    }

    public class GenericTypeComparer : IComparer<string>
    {
        Type t;

        public GenericTypeComparer(Type _t)
        {
            t = _t;
        }

        public int Compare(string x, string y)
        {
            if (typeof(IComparable).IsAssignableFrom(t))
            {
                var converter = TypeDescriptor.GetConverter(t);
                var xx = converter.ConvertFrom(x);
                var yy = converter.ConvertFrom(y);
                return ((IComparable)xx).CompareTo(yy);
            }
            return x.CompareTo(y);
        }
    }
}
