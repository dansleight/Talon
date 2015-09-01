using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public class ButtonColumnBuilder<T> : ColumnBuilderBase<IColumn, ButtonColumnBuilder<T>> where T : class
    {

        #region Properties

        private ButtonColumn<T> component;
        protected internal ButtonColumn<T> Component
        {
            get { return this.component; }
            set { this.component = value; }
        }

        #endregion

        #region Constructor

        public ButtonColumnBuilder(ButtonColumn<T> column)
            : base(column)
        {
            this.component = column;
        }

        #endregion

        #region Methods

        public ButtonColumnBuilder<T> NoWrap()
        {
            this.Column.ClassName += " talon-nowrap";
            return this;
        }

        #endregion
    }
}
