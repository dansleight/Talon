using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public class CustomColumn<TModel> : ColumnBase<TModel>, IColumn where TModel : class
    {
        #region variables



        #endregion

        #region Properties

        public string TemplateString { get; set; }

        #endregion

        #region Constructor

        public CustomColumn(Grid<TModel> grid, string template)
            : base (grid, ColumnType.Custom)
        {
            Template = template;
        }

        #endregion
    }
}
