using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public class ColumnButtonBuilder
    {
        #region Properties

        public ColumnButton Button { get; private set; }

        #endregion

        #region Constructor

        public ColumnButtonBuilder(ColumnButton button)
        {
            this.Button = button;
        }

        #endregion

        #region Methods

        public ColumnButtonBuilder Type(string type)
        {
            this.Button.Type = type;
            return this;
        }

        public ColumnButtonBuilder Title(string title)
        {
            this.Button.Title = title;
            return this;
        }

        public ColumnButtonBuilder Icon(string icon)
        {
            this.Button.Icon = icon;
            return this;
        }

        #endregion
    }
}
