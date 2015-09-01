using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public class ButtonColumn<TModel> : ColumnBase<TModel>, IColumn where TModel : class
    {
        #region variables



        #endregion

        #region Properties

        public List<ColumnButton> Buttons { get; set; }

        #endregion

        #region Constructor

        public ButtonColumn(Grid<TModel> grid)
            : base (grid, ColumnType.Button)
        {
            ClassName = "talon-buttons";
            Buttons = new List<ColumnButton>();
        }

        #endregion
    }
}
