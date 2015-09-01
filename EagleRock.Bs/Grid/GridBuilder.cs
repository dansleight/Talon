using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EagleRock.Bs.Grid
{
    public class GridBuilder<T> : IHtmlString where T : class
    {
        #region Properties

        private Grid<T> component;
        protected internal Grid<T> Component
        {
            get { return this.component; }
            set { this.component = value; }
        }

        #endregion

        #region Constructor

        public GridBuilder(Grid<T> component)
        {
            this.component = component;
        }

        #endregion

        #region Object Methods

        public GridBuilder<T> Name(string gridName)
        {
            this.Component.Name = gridName;
            return this;
        }

        public GridBuilder<T> AjaxSource(string route)
        {
            this.Component.AjaxSource = route;
            return this;
        }

        public GridBuilder<T> Columns(Action<ColumnFactory<T>> configurator)
        {
            ColumnFactory<T> gridColumnFactory = new ColumnFactory<T>(this.Component);
            configurator(gridColumnFactory);
            return this;
        }

        public GridBuilder<T> Paginate(bool paginate)
        {
            this.Component.Paginate = paginate;
            return this;
        }

        public GridBuilder<T> PagingType(PagingType pagingType)
        {
            this.Component.PagingType = pagingType;
            return this;
        }

        public GridBuilder<T> PageSizes(List<int> pageSizes)
        {
            this.Component.PageSizes = pageSizes;
            return this;
        }

        public GridBuilder<T> PageSize(int pageSize)
        {
            this.Component.PageSize = pageSize;
            return this;
        }

        public GridBuilder<T> PageSize(PageSize pageSize)
        {
            this.Component.PageSize = -1;
            return this;
        }

        public GridBuilder<T> Clean()
        {
            this.Component.Clean = true;
            return this;
        }

        public GridBuilder<T> ScrollX()
        {
            this.Component.ScrollX = true;
            return this;
        }

        public GridBuilder<T> ScrollX(bool scrollX)
        {
            this.Component.ScrollX = scrollX;
            return this;
        }

        public GridBuilder<T> ScrollY()
        {
            this.Component.ScrollY = "300px";
            return this;
        }

        public GridBuilder<T> ScrollY(int px)
        {
            this.Component.ScrollY = px.ToString() + "px";
            return this;
        }

        public GridBuilder<T> ScrollY(string scrollY)
        {
            this.Component.ScrollY = scrollY;
            return this;
        }

        public GridBuilder<T> Freeze(int freeze)
        {
            this.Component.Freeze = freeze;
            return this;
        }

        public GridBuilder<T> Freeze()
        {
            this.Component.Freeze = 1;
            return this;
        }

        public void SetDataSource(IEnumerable<T> dataSource)
        {
            this.Component.DataSource = dataSource;
        }

        public string ToHtmlString()
        {
            return this.Component.ToHtmlString();
        }

        #endregion

    }
}
