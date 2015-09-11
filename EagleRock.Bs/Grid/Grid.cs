using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace EagleRock.Bs.Grid
{
    public class Grid<T> where T : class
    {
        #region Properties

        public IDictionary<string, object> TableHtmlAttributes { get; private set; }

        public IList<ColumnBase<T>> Columns { get; set; }

        public IEnumerable<T> DataSource { get; set; }

        public string Name { get; set; }

        public ViewContext ViewContext { get; private set; }

        public string AjaxSource { get; set; }

        public bool Paginate { get; set; }

        public PagingType PagingType { get; set; }

        public List<int> PageSizes { get; set; }

        public int PageSize { get; set; }

        public bool Clean { get; set; }

        public int Freeze { get; set; }

        public bool ScrollX { get; set; }

        public string ScrollY { get; set; }

        #endregion

        #region Constructor

        public Grid(ViewContext viewContext)
        {
            this.Columns = new List<ColumnBase<T>>();
            this.TableHtmlAttributes = (IDictionary<string, object>)new RouteValueDictionary();
            this.PagingType = PagingType.simple_numbers;
            this.Paginate = true;
        }

        #endregion

        #region Static Methods

        public static string getPropertyValue(object src, ColumnBase<T> col)
        {
            // this really exists with only minor differences in two places, and we should consider a way to merge them into one
            try
            {
                if (!String.IsNullOrWhiteSpace(col.Template))
                {
                    // this is a custom column that needs processed
                    string toReturn = col.Template;
                    Regex regex = new Regex("{.*?}");
                    var toProcess = regex.Matches(col.Template);
                    foreach (var match in toProcess)
                    {
                        try
                        {
                            string matching = match.ToString();
                            string prop = matching.Trim(new char[] { '{', '}' });
                            string val = src.GetType().GetProperty(prop).GetValue(src, null).ToString();
                            toReturn = toReturn.Replace(matching, val);
                        }
                        catch { }
                    }
                    return toReturn;
                }
                else
                {
                    if (col.Type == null)
                        col.Type = src.GetType().GetProperty(col.Member).PropertyType;
                    if (col.Type == typeof(DateTime) && !string.IsNullOrWhiteSpace(col.Format))
                    {
                        return ((DateTime)src.GetType().GetProperty(col.Member).GetValue(src, null)).ToString(col.Format);
                    }
                    else if (col.Type == typeof(DateTime?) && !string.IsNullOrWhiteSpace(col.Format))
                    {
                        if (((DateTime?)src.GetType().GetProperty(col.Member).GetValue(src, null)).HasValue)
                        {
                            return ((DateTime?)src.GetType().GetProperty(col.Member).GetValue(src, null)).Value.ToString(col.Format);
                        }
                    }
                    return src.GetType().GetProperty(col.Member).GetValue(src, null).ToString();
                }
            }
            catch { }
            return string.Empty;
        }

        #endregion

        #region Object Methods

        protected void WriteHtml(HtmlTextWriter writer)
        {
            var jsName = this.Name.Replace("-", "__");

            // create the table
            writer.WriteLine(String.Format("<table id=\"{0}\" class=\"table table-striped table-bordered\" cellspacing=\"0\" width=\"100%\"></table>", this.Name));

            int index = 0;
            foreach (var col in this.Columns)
                col.Index = index++;

            // if we're serializing data, write it out here...
            // TODO, limit the serializaton to the bound columns
            if (this.DataSource != null)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Script);
                // TODO, convert this object, it will need to add orthagonal data, and handle custom column rendering
                List<Dictionary<string, dynamic>> processed = new List<Dictionary<string, object>>();
                foreach (var row in this.DataSource)
                {
                    Dictionary<string, dynamic> addRow = new Dictionary<string, dynamic>();
                    foreach (var col in this.Columns)
                    {
                        if (col.ColumnType == ColumnType.Render || col.ColumnType == ColumnType.Button)
                            continue;
                        else
                        {
                            string val = getPropertyValue(row, col);
                            if (!String.IsNullOrWhiteSpace(col.Template))
                                addRow.Add("CustomColumn_" + col.Index.ToString(), val);
                            else
                            {
                                Type t = row.GetType().GetProperty(col.Member).PropertyType;
                                if (t == typeof(DateTime))
                                {
                                    // going to send orthogonal
                                    DateTime realVal = (DateTime)row.GetType().GetProperty(col.Member).GetValue(row, null);
                                    addRow.Add(col.Member, new { display = val, timestamp = realVal.ToString("yyyyMMddhhmmss") });
                                }
                                else
                                {
                                    addRow.Add(col.Member, val);
                                }
                            }
                        }
                    }
                    processed.Add(addRow);
                }


                writer.WriteLine("var " + jsName + "_Data = " + Json.Encode(processed));
                writer.RenderEndTag();
            }

            // begin creating the datatable initialiaztion script
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            writer.WriteLine(String.Format("$(document).ready(function () {{ var {1} = $('#{0}').DataTable({{", this.Name, jsName));

            if (this.Paginate || this.Clean)
            {
                // paging type
                if (PagingType != PagingType.simple_numbers)
                {
                    writer.WriteLine("pagingType: '" + PagingType.ToString() + "',");
                }

                // page sizes
                if (PageSizes != null && PageSizes.Count() > 0)
                {
                    writer.Write("lengthMenu: [[");
                    writer.Write(string.Join(",", PageSizes));
                    writer.Write("],[");
                    writer.Write(string.Join(",", PageSizes).Replace("-1", "'All'"));
                    writer.WriteLine("]],");
                }

                // page size
                if (PageSize != 0)
                {
                    writer.WriteLine("displayLength: " + PageSize + ",");
                }
            }
            else
            {
                writer.WriteLine("paging: false,");
            }

            if (this.Clean)
            {
                writer.WriteLine("sDom: 't',");
            }

            // ScrollX
            if (ScrollX || Freeze > 0)
            {
                writer.WriteLine("scrollX: true,");
            }

            // ScrollY
            if (!String.IsNullOrEmpty(ScrollY))
            {
                writer.WriteLine("scrollY: '" + ScrollY + "',");
                writer.WriteLine("scrollCollapse: true,");
            }

            writer.WriteLine("columns: [");

            StringBuilder dataMethods = new StringBuilder();
            int colIndex = 0;
            // loop through the columns
            foreach (var c in this.Columns)
            {
                c.Index = colIndex;
                c.ProcessResponsive();

                writer.Write("{");

                if (c.ColumnType == ColumnType.Bound)
                {
                    #region Bound
                    if (String.IsNullOrEmpty(c.Title))
                    {
                        c.Title = StringHelper.SplitPascalCase(c.Member);
                    }

                    if (c.Type == typeof(DateTime))
                    {
                        writer.Write(string.Format(" data: \"{0}.display\", sort: \"{0}.timestamp\", ", c.Member));
                    }
                    else
                    {
                        writer.Write(String.Format(" data: \"{0}\", ", c.Member));
                    }

                    if (c.Width > 0)
                    {
                        writer.Write(string.Format("width: {0}, ", c.Width));
                    }


                    writer.Write(string.Format("title: \"{0}\", ", c.Title));
                    if (!String.IsNullOrEmpty(c.Format))
                    {
                        dataMethods.AppendLine(String.Format("d.columns[{0}][\"format\"] = '{1}';", colIndex, c.Format));
                    }
                    if (!String.IsNullOrWhiteSpace(c.Render))
                    {
                        writer.Write(String.Format("render: {0}, ", c.Render));
                    }
                    // var x = typeof(T).GetProperty(c.Member).PropertyType.Name;
                    #endregion
                }
                else if (c.ColumnType == ColumnType.Custom)
                {
                    #region Custom
                    c.Member = "CustomColumn_" + colIndex.ToString();
                    if (String.IsNullOrEmpty(c.Title))
                    {
                        c.Title = "Custom";
                    }
                    writer.Write(String.Format(" data: \"{0}\", title: \"{1}\", ", c.Member, c.Title));
                    // c.Searchable = false;
                    // c.Sortable = false;

                    dataMethods.AppendLine(String.Format("d.columns[{0}][\"template\"] = '{1}';", colIndex, HttpUtility.HtmlEncode(c.Template)));

                    #endregion
                }
                else if (c.ColumnType == ColumnType.Render)
                {
                    #region Render
                    var rc = (RenderColumn<T>)c;
                    if (String.IsNullOrEmpty(c.Title))
                    {
                        c.Title = "Render";
                    }
                    writer.Write(String.Format(" render: {0}, title: \"{1}\", ", rc.RenderMethod, c.Title));
                    #endregion
                }
                else if (c.ColumnType == ColumnType.Button)
                {
                    #region Button
                    List<dynamic> buttonDefs = new List<dynamic>();
                    foreach (var b in ((ButtonColumn<T>)c).Buttons)
                    {
                        buttonDefs.Add(b);
                    }
                    string renderJs = string.Format("function (data, type, row) {{ return talondatatable_renderbuttons(row, {0}); }}", Json.Encode(buttonDefs));
                    // addRow.Add("ButtonColumn_" + col.Index.ToString(), buttonDefs);
                    writer.Write(String.Format(" data: \"ButtonColumn_{0}\", title: \"{1}\", render: {2}, ", c.Index, c.Title, renderJs));
                    #endregion
                }

                if (!String.IsNullOrEmpty(c.ClassName))
                {
                    writer.Write(String.Format("className: \"{0}\", ", c.ClassName));
                }
                if (c.Hidden)
                {
                    writer.Write("visible: false, ");
                }
                if (c.Width > 0)
                {
                    writer.Write(String.Format("width: {0}, ", c.Width));
                }
                if (!c.Searchable)
                {
                    writer.Write("searchable: false, ");
                }
                if (!c.Sortable)
                {
                    writer.Write("sortable: false, ");
                }

                writer.WriteLine("},");

                colIndex++;
            }
            // finish the initialization script
            writer.WriteLine("],");

            var orders = this.Columns.Where(c => c.SortOrder > 0);

            if (orders.Count() > 0)
            {
                writer.Write("order: [");
                foreach (var col in orders.OrderBy(o => o.SortOrder))
                {
                    writer.Write("[ " + col.Index + ", '" + col.SortDirection.ToString() + "' ],");
                }
                writer.WriteLine("],");
            }

            if (this.DataSource != null)
            {
                writer.WriteLine("data: " + jsName + "_Data,");
            }
            else if (!String.IsNullOrEmpty(this.AjaxSource))
            {
                writer.WriteLine("processing: true, serverSide: true, searchDelay: 750, ajax: {");
                writer.WriteLine("url: \"" + this.AjaxSource + "\", type: \"POST\",");
                if (dataMethods.Length > 0)
                {
                    writer.WriteLine("data: function(d) {");
                    writer.WriteLine(dataMethods.ToString());
                    writer.WriteLine("}");
                }
                writer.WriteLine(" },");
            }

            writer.WriteLine("});");

            if (Freeze > 0)
            {
                writer.WriteLine(String.Format("new $.fn.dataTable.FixedColumns( {0}, {{ leftColumns: {1} }});", jsName, Freeze));
            }

            writer.WriteLine(String.Format("$('#{0}').data('datatable', {1});", this.Name, jsName));

            writer.WriteLine("});");

            writer.RenderEndTag();
        }

        public string ToHtmlString()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                this.WriteHtml(new HtmlTextWriter((TextWriter)stringWriter));
                return stringWriter.ToString();
            }
        }

        #endregion
    }
}
