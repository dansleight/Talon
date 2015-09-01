using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public class ColumnButton
    {
        #region Properties

        public string Method { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Type { get; set; }
        public bool IconOnly { get; set; }

        #endregion

        #region Constructor

        public ColumnButton(string method)
        {
            this.Method = method;
        }

        public ColumnButton(string method, string icon)
        {
            this.Method = method;
            this.Icon = icon;
        }

        public ColumnButton(string method, string icon, string type)
        {
            this.Method = method;
            this.Icon = icon;
            this.Type = type;
        }

        public ColumnButton(string method, string icon, string type, string title)
        {
            this.Method = method;
            this.Title = title;
            this.Type = type;
            this.Icon = icon;
        }

        #endregion
    }
}
