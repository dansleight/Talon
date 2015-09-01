using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public class RenderColumn<TModel> : ColumnBase<TModel>, IColumn where TModel : class
    {
        #region variables



        #endregion

        #region Properties

        public string RenderMethod { get; set; }

        #endregion

        #region Constructor

        public RenderColumn(Grid<TModel> grid, string renderMethod)
            : base(grid, ColumnType.Render)
        {
            this.RenderMethod = renderMethod;
        }

        #endregion
    }
}
