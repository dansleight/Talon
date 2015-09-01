using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public abstract class ColumnBuilderBase<TColumn, TColumnBuilder> where TColumn : IColumn where TColumnBuilder : ColumnBuilderBase<TColumn, TColumnBuilder>
    {
        #region Properties

        public TColumn Column { get; private set; }

        #endregion

        #region Constructor

        protected ColumnBuilderBase(TColumn column)
        {
            this.Column = column;
            if (this.Column.ClassName == null)
                this.Column.ClassName = "";
        }

        #endregion

        #region Methods

        public TColumnBuilder Title(string title)
        {
            this.Column.Title = title;
            return (TColumnBuilder)this;
        }

        public TColumnBuilder AlignLeft()
        {
            this.Column.ClassName += " text-left";
            return (TColumnBuilder)this;
        }

        public TColumnBuilder AlignCenter()
        {
            this.Column.ClassName += " text-center";
            return (TColumnBuilder)this;
        }

        public TColumnBuilder AlignRight()
        {
            this.Column.ClassName += " text-right";
            return (TColumnBuilder)this;
        }

        public TColumnBuilder ClassName(string className)
        {
            this.Column.ClassName += " " + className;
            return (TColumnBuilder)this;
        }

        public TColumnBuilder Width(int width)
        {
            this.Column.Width = width;
            return (TColumnBuilder)this;
        }

        public TColumnBuilder ResponsiveHide(ColumnResponsiveSize responsiveSize = ColumnResponsiveSize.sm)
        {
            this.Column.ResponsiveAction = ColumnResponsiveAction.Hide;
            this.Column.ResponsiveSize = responsiveSize;
            return (TColumnBuilder)this;
        }

        public TColumnBuilder ResponsiveShow(ColumnResponsiveSize responsiveSize = ColumnResponsiveSize.sm)
        {
            this.Column.ResponsiveAction = ColumnResponsiveAction.Show;
            this.Column.ResponsiveSize = responsiveSize;
            return (TColumnBuilder)this;
        }

        #endregion
    }
}
