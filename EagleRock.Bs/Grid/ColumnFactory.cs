using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace EagleRock.Bs.Grid
{
    public class ColumnFactory<TModel> where TModel : class
    {
        #region Properties

        public Grid<TModel> Container { get; private set; }

        #endregion

        #region Constructor

        public ColumnFactory(Grid<TModel> container)
        {
            this.Container = container;
        }

        #endregion

        #region Object Methods

        public virtual BoundColumnBuilder<TModel> Bound<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            BoundColumn<TModel, TValue> boundColumn = new BoundColumn<TModel, TValue>(this.Container, expression);
            this.Container.Columns.Add((ColumnBase<TModel>) boundColumn);
            return new BoundColumnBuilder<TModel>((IColumn)boundColumn);
        }

        public virtual CustomColumnBuilder<TModel> Custom(string templateString)
        {
            CustomColumn<TModel> customColumn = new CustomColumn<TModel>(this.Container, templateString);
            this.Container.Columns.Add((ColumnBase<TModel>)customColumn);
            return new CustomColumnBuilder<TModel>((IColumn)customColumn);
        }

        public virtual CustomColumnBuilder<TModel> Custom(Func<object, HelperResult> htmlTemplate)
        {
            string templateString = htmlTemplate.Invoke(null).ToString();
            CustomColumn<TModel> customColumn = new CustomColumn<TModel>(this.Container, templateString);
            this.Container.Columns.Add((ColumnBase<TModel>)customColumn);
            return new CustomColumnBuilder<TModel>((IColumn)customColumn);
        }

        public virtual RenderColumnBuilder<TModel> Render(string renderMethod)
        {
            RenderColumn<TModel> renderColumn = new RenderColumn<TModel>(this.Container, renderMethod);
            this.Container.Columns.Add((ColumnBase<TModel>)renderColumn);
            return new RenderColumnBuilder<TModel>((IColumn)renderColumn);
        }

        public virtual ButtonColumnBuilder<TModel> Buttons(Action<ButtonFactory<TModel>> configurator)
        {
            ButtonColumn<TModel> buttonColumn = new ButtonColumn<TModel>(this.Container);
            this.Container.Columns.Add((ColumnBase<TModel>)buttonColumn);

            ButtonFactory<TModel> buttonColumnFactory = new ButtonFactory<TModel>(buttonColumn);
            configurator(buttonColumnFactory);
            return new ButtonColumnBuilder<TModel>(buttonColumn);
        }

        #endregion
    }
}
