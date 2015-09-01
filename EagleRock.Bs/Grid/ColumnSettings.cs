using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace EagleRock.Bs.Grid
{
    public class ColumnSettings
    {
        #region variables

        private string clientTemplate;

        #endregion

        #region Properties

        public IDictionary<string, object> HtmlAttributes { get; private set; }

        public string Member { get; set; }

        public string Title { get; set; }

        public string Format { get; set; }

        public bool Hidden { get; set; }

        public int Width { get; set; }

        public bool Sortable { get; set; }

        public string ClassName { get; set; }

        public bool Searchable { get; set; }

        public int SortOrder { get; set; }

        public SortDir SortDirection { get; set; }

        public ColumnResponsiveAction? ResponsiveAction { get; set; }

        public ColumnResponsiveSize? ResponsieSize { get; set; }

        public string Render { get; set; }

        #endregion

        #region Constructor

        public ColumnSettings()
        {
            this.Sortable = true;
            this.HtmlAttributes = (IDictionary<string, object>)new RouteValueDictionary();
        }

        #endregion

    }
}
