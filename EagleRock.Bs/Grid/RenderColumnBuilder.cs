using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public class RenderColumnBuilder<T> : ColumnBuilderBase<IColumn, RenderColumnBuilder<T>> where T : class
    {
        #region Constructor

        public RenderColumnBuilder(IColumn column)
            : base(column)
        { }

        #endregion

        #region Methods



        #endregion
    }
}
