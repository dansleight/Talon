using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public class CustomColumnBuilder<T> : ColumnBuilderBase<IColumn, CustomColumnBuilder<T>> where T : class
    {
        #region Constructor

        public CustomColumnBuilder(IColumn column)
            : base(column)
        {

        }

        #endregion
    }
}
