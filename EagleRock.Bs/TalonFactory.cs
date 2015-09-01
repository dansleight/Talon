using EagleRock.Bs.CKEditor;
using EagleRock.Bs.DatePicker;
using EagleRock.Bs.Summernote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace EagleRock.Bs
{
    public class TalonFactory<TModel>
    {
        #region Properties

        [EditorBrowsable(EditorBrowsableState.Never)]
        public HtmlHelper<TModel> HtmlHelper { get; set; }

        private ViewContext ViewContext { get { return this.HtmlHelper.ViewContext; } }

        #endregion

        #region Constructor

        public TalonFactory(HtmlHelper<TModel> htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
        }

        #endregion

        #region Grid

        public Grid.GridBuilder<T> Grid<T>() where T : class
        {
            return new Grid.GridBuilder<T>(new Grid.Grid<T>(this.ViewContext));
        }

        public Grid.GridBuilder<T> Grid<T>(IEnumerable<T> dataSource) where T : class
        {
            Grid.GridBuilder<T> builder = this.Grid<T>();
            builder.SetDataSource(dataSource);
            return builder;
        }

        #endregion

        #region Summernote

        public SummernoteBuilder Summernote(string name)
        {
            SummernoteBuilder builder = new SummernoteBuilder(new Summernote.Summernote(this.ViewContext, name));
            return builder;
        }

        #endregion

        #region CKEditor

        public CKEditorBuilder CKEditor(string name)
        {
            CKEditorBuilder builder = new CKEditorBuilder(new Bs.CKEditor.CKEditor(this.ViewContext, name));
            return builder;
        }

        #endregion

        #region DatePicker

        public DatePicker.DatePickerBuilder DatePickerFor<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return DatePickerFor(expression, dict);
        }

        public DatePicker.DatePickerBuilder DatePickerFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return DatePickerFor(expression, htmlAttributes);
        }

        public DatePicker.DatePickerBuilder DatePickerFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            ViewDataDictionary<TModel> viewData = new ViewDataDictionary<TModel>(HtmlHelper.ViewData);
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, viewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            string labelText = metadata.DisplayName;
            if (labelText == null)
            {
                labelText = BsHelper.SplitCamelCase(metadata.PropertyName ?? htmlFieldName.Split('.').Last());
            }

            // add a placeholder if it isn't already there
            if (!String.IsNullOrEmpty(labelText))
            {
                if (!htmlAttributes.ContainsKey("placeholder"))
                {
                    htmlAttributes["placeholder"] = "{placeholder}";
                }
            }

            // add form-control
            if (htmlAttributes.ContainsKey("class"))
            {
                htmlAttributes["class"] += " form-control date";
            }
            else
            {
                htmlAttributes["class"] = "form-control date";
            }


            DateTime? dtval = null;
            string val = HtmlHelper.ValueFor<TModel, TProperty>(expression).ToString();
            string input = HtmlHelper.TextBoxFor(expression, htmlAttributes ).ToString();
            if (val.Length > 0)
            {
                input = input.Replace(val, "{value}");
                DateTime temp;
                if (DateTime.TryParse(val, out temp))
                    dtval = temp;
            }


            DatePicker.DatePicker component = new DatePicker.DatePicker(htmlFieldName);
            component.DisplayName = labelText;

            DatePickerBuilder builder = new DatePickerBuilder(component, input, dtval);
            return builder;
        }

        #endregion

        #region LabelFor

        public MvcHtmlString LabelFor<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return LabelFor(expression, new RouteValueDictionary(htmlAttributes));
        }

        public MvcHtmlString LabelFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return LabelFor(expression, new RouteValueDictionary(new Dictionary<string, object>()));
        }

        public MvcHtmlString LabelFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            int labelSize = BsHelper.GetLabelSize();

            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }

            if (htmlAttributes.ContainsKey("class"))
            {
                htmlAttributes["class"] += " control-label col-sm-" + labelSize.ToString();
            }
            else
            {
                htmlAttributes["class"] = "control-label col-sm-" + labelSize.ToString();
            }

            ViewDataDictionary<TModel> viewData = new ViewDataDictionary<TModel>(HtmlHelper.ViewData);

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, viewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            if (metadata.DisplayName == null)
                metadata.DisplayName = BsHelper.SplitCamelCase(metadata.PropertyName ?? htmlFieldName.Split('.').Last());

            return HtmlHelper.LabelFor(expression, metadata.DisplayName, htmlAttributes);
        }

        #endregion

        #region TextBoxFor

        public MvcHtmlString TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return TextBoxFor(expression, dict);
        }

        public MvcHtmlString TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return TextBoxFor(expression, htmlAttributes);
        }

        public MvcHtmlString TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            // make sure that htmlAttributes is not null, or we won't be able to add to it
            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }

            // get the field name for use with bootstrap
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName;
            if (labelText == null)
            {
                labelText = BsHelper.SplitCamelCase(metadata.PropertyName ?? htmlFieldName.Split('.').Last());
            }

            // add a placeholder if it isn't already there
            if (!String.IsNullOrEmpty(labelText))
            {
                if (!htmlAttributes.ContainsKey("placeholder"))
                {
                    htmlAttributes["placeholder"] = labelText;
                }
            }

            // add form-control
            if (htmlAttributes.ContainsKey("class"))
            {
                htmlAttributes["class"] += " form-control";
            }
            else
            {
                htmlAttributes["class"] = "form-control";
            }
            return HtmlHelper.TextBoxFor(expression, htmlAttributes);
        }

        #endregion

        #region PasswordFor

        public MvcHtmlString PasswordFor<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return PasswordFor(expression, dict);
        }

        public MvcHtmlString PasswordFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return PasswordFor(expression, htmlAttributes);
        }

        public MvcHtmlString PasswordFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            // make sure that htmlAttributes is not null, or we won't be able to add to it
            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }

            // get the field name for use with bootstrap
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName;
            if (labelText == null)
            {
                labelText = BsHelper.SplitCamelCase(metadata.PropertyName ?? htmlFieldName.Split('.').Last());

            }

            // add a placeholder if it isn't already there
            if (!String.IsNullOrEmpty(labelText))
            {
                if (!htmlAttributes.ContainsKey("placeholder"))
                {
                    htmlAttributes["placeholder"] = labelText;
                }
            }

            // add form-control
            if (htmlAttributes.ContainsKey("class"))
            {
                htmlAttributes["class"] += " form-control";
            }
            else
            {
                htmlAttributes["class"] = "form-control";
            }

            return HtmlHelper.PasswordFor(expression, htmlAttributes);
        }


        #endregion

        #region TextAreaFor
        public MvcHtmlString TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return TextAreaFor(expression, dict);
        }

        public MvcHtmlString TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return TextAreaFor(expression, htmlAttributes);
        }

        public MvcHtmlString TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            // make sure that htmlAttributes is not null, or we won't be able to add to it
            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }

            // get the field name for use with bootstrap
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName;
            if (labelText == null)
            {
                labelText = BsHelper.SplitCamelCase(metadata.PropertyName ?? htmlFieldName.Split('.').Last());
            }

            // add a placeholder if it isn't already there
            if (!String.IsNullOrEmpty(labelText))
            {
                if (!htmlAttributes.ContainsKey("placeholder"))
                {
                    htmlAttributes["placeholder"] = labelText;
                }
            }

            // add form-control
            if (htmlAttributes.ContainsKey("class"))
            {
                htmlAttributes["class"] += " form-control";
            }
            else
            {
                htmlAttributes["class"] = "form-control";
            }

            return HtmlHelper.TextAreaFor(expression, htmlAttributes);

        }

        #endregion

        #region DropDownListFor

        public MvcHtmlString DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return DropDownListFor(expression, selectListItems, null, htmlAttributes);
        }

        public MvcHtmlString DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return DropDownListFor(expression, selectListItems, null, dict);
        }

        public MvcHtmlString DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems, string optionLabel)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return DropDownListFor(expression, selectListItems, optionLabel, htmlAttributes);
        }

        public MvcHtmlString DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems, IDictionary<string, object> htmlAttributes)
        {
            return DropDownListFor(expression, selectListItems, null, htmlAttributes);
        }

        public MvcHtmlString DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems, string optionLabel, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return DropDownListFor(expression, selectListItems, optionLabel, dict);
        }

        public MvcHtmlString DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItems, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            // make sure that htmlAttributes is not null, or we won't be able to add to it
            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }

            // add span_select to the class for bootstrap
            if (htmlAttributes.ContainsKey("class"))
            {
                htmlAttributes["class"] += " form-control";
            }
            else
            {
                htmlAttributes.Add("class", "form-control");
            }
            return HtmlHelper.DropDownListFor(expression, selectListItems, optionLabel, htmlAttributes);
        }

        #endregion

        #region DisplayFor

        public MvcHtmlString DisplayFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression).Split('.').LastOrDefault();
            if (metadata.DisplayName == null)
            {
                metadata.DisplayName = BsHelper.SplitCamelCase(metadata.PropertyName ?? htmlFieldName.Split('.').Last());
            }

            MvcHtmlString expected = HtmlHelper.DisplayFor(expression);

            if (expression.ReturnType == typeof(Boolean) || expression.ReturnType == typeof(Boolean?))
            {
                if (expected.ToString().Contains("checked")) return new MvcHtmlString("Yes");
                return new MvcHtmlString("No");
            }
            else if (expression.ReturnType == typeof(DateTime) || expression.ReturnType == typeof(DateTime?))
            {
                DateTime testDate;
                if (DateTime.TryParse(expected.ToString(), out testDate))
                {
                    // so we have a date, but we want it to look good
                    // if the time is midnight, we don't need to show the time
                    if (testDate.TimeOfDay.Ticks == 0)
                        return new MvcHtmlString(testDate.ToString("MMMM d, yyyy"));
                    else
                        return new MvcHtmlString(testDate.ToString("MMMM d, yyyy H:mm tt"));
                }
            }
            else if (expression.ReturnType == typeof(SelectList))
            {
                SelectList value = (SelectList)metadata.Model;
                var items = value.AsEnumerable();

                if (items.Count() == 0)
                    return new MvcHtmlString(String.Format("<em class='undefined-display'>No {0} Defined</em>", metadata.DisplayName));

                string toReturn = "<ul>";
                foreach (var item in items)
                {
                    toReturn += "<li>" + item.Text + "</li>";
                }
                toReturn += "</ul>";

                return new MvcHtmlString(toReturn);
            }

            if (!String.IsNullOrWhiteSpace(expected.ToString())) return expected;

            return new MvcHtmlString(String.Format("<em class='undefined-display'>No {0} Defined</em>", metadata.DisplayName));
        }

        #endregion
    }
}
