using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EagleRock.Bs.Grid
{
    public class BoundColumnBuilder<T> : ColumnBuilderBase<IColumn, BoundColumnBuilder<T>> where T : class
    {
        #region Constructor

        public BoundColumnBuilder(IColumn column)
            : base(column)
        {
        }

        #endregion

        #region Methods

        public BoundColumnBuilder<T> Sortable(bool value)
        {
            this.Column.Sortable = value;
            return this;
        }

        public BoundColumnBuilder<T> Order()
        {
            this.Column.SortOrder = 1;
            this.Column.SortDirection = SortDir.asc;
            return this;
        }

        public BoundColumnBuilder<T> Order(int value)
        {
            this.Column.SortOrder = value;
            this.Column.SortDirection = SortDir.asc;
            return this;
        }

        public BoundColumnBuilder<T> Order(SortDir sortDirection)
        {
            this.Column.SortOrder = 1;
            this.Column.SortDirection = sortDirection;
            return this;
        }

        public BoundColumnBuilder<T> Order(int value, SortDir sortDirection)
        {
            this.Column.SortOrder = value;
            this.Column.SortDirection = sortDirection;
            return this;
        }

        public BoundColumnBuilder<T> Hidden(bool value = true)
        {
            this.Column.Hidden = value;
            return this;
        }

        public BoundColumnBuilder<T> Searchable(bool value = true)
        {
            this.Column.Searchable = value;
            return this;
        }

        public BoundColumnBuilder<T> Format(string format)
        {
            this.Column.Format = format;
            return this;
        }

        public BoundColumnBuilder<T> Render(string render)
        {
            this.Column.Render = render;
            return this;
        }

        #endregion
    }
}
