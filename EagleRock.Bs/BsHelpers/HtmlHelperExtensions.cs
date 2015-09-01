using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace EagleRock.Bs.BsHelpers
{
    public static class HtmlHelperExtensions
    {
        #region BsLabelFor

        public static MvcHtmlString BsLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return BsLabelFor(htmlHelper, expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString BsLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return BsLabelFor(htmlHelper, expression, new RouteValueDictionary(new Dictionary<string, object>()));
        }

        public static MvcHtmlString BsLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            TalonFactory<TModel> talon = new TalonFactory<TModel>(htmlHelper);
            return talon.LabelFor(expression, htmlAttributes);
        }

        #endregion

        #region BsTextBoxFor

        public static MvcHtmlString BsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return BsTextBoxFor(htmlHelper, expression, dict);
        }

        public static MvcHtmlString BsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return BsTextBoxFor(htmlHelper, expression, htmlAttributes);
        }

        public static MvcHtmlString BsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            TalonFactory<TModel> talon = new TalonFactory<TModel>(htmlHelper);
            return talon.TextBoxFor(expression, htmlAttributes);
        }

        #endregion

        #region BsPasswordFor

        public static MvcHtmlString BsPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return BsPasswordFor(htmlHelper, expression, dict);
        }

        public static MvcHtmlString BsPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return BsPasswordFor(htmlHelper, expression, htmlAttributes);
        }

        public static MvcHtmlString BsPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            TalonFactory<TModel> talon = new TalonFactory<TModel>(htmlHelper);
            return talon.PasswordFor(expression, htmlAttributes);
        }


        #endregion

        #region BsTextAreaFor
        public static MvcHtmlString BsTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return BsTextAreaFor(htmlHelper, expression, dict);
        }

        public static MvcHtmlString BsTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return BsTextAreaFor(htmlHelper, expression, htmlAttributes);
        }

        public static MvcHtmlString BsTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            TalonFactory<TModel> talon = new TalonFactory<TModel>(htmlHelper);
            return talon.TextAreaFor(expression, htmlAttributes);
        }

        #endregion

        #region BsDropDownListFor

        public static MvcHtmlString BsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return BsDropDownListFor(htmlHelper, expression, selectListItems, null, htmlAttributes);
        }

        public static MvcHtmlString BsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return BsDropDownListFor(htmlHelper, expression, selectListItems, null, dict);
        }

        public static MvcHtmlString BsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems, string optionLabel)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return BsDropDownListFor(htmlHelper, expression, selectListItems, optionLabel, htmlAttributes);
        }

        public static MvcHtmlString BsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems, IDictionary<string, object> htmlAttributes)
        {
            return BsDropDownListFor(htmlHelper, expression, selectListItems, null, htmlAttributes);
        }

        public static MvcHtmlString BsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems, string optionLabel, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return BsDropDownListFor(htmlHelper, expression, selectListItems, optionLabel, dict);
        }

        public static MvcHtmlString BsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            TalonFactory<TModel> talon = new TalonFactory<TModel>(htmlHelper);
            return talon.DropDownListFor(expression, selectListItems, optionLabel, htmlAttributes);
        }

        #endregion

        #region BsDisplayFor

        public static MvcHtmlString BsDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            TalonFactory<TModel> talon = new TalonFactory<TModel>(htmlHelper);
            return talon.DisplayFor(expression);
        }

        #endregion
    }
}
