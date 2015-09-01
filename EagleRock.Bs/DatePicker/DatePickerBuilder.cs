using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EagleRock.Bs.DatePicker
{
    public class DatePickerBuilder : IHtmlString
    {
        #region Properties

        private DatePicker _component;

        protected internal DatePicker Component
        {
            get { return this._component; }
            set { this._component = value; }
        }

        protected internal string Input { get; set; }
        protected internal DateTime? Value { get; set; }

        #endregion

        #region Constructor

        public DatePickerBuilder(DatePicker component, string input, DateTime? value)
        {
            this.Component = component;
            this.Input = input;
            this.Value = value;
        }

        #endregion

        #region Object Methods

        public DatePickerBuilder AsComponent(bool asComponent)
        {
            _component.AsComponent = asComponent;
            return this;
        }

        public DatePickerBuilder Orientation(DPOrientation orientation)
        {
            _component.Orientation = orientation;
            return this;
        }

        public DatePickerBuilder Format(string format)
        {
            _component.Format = format;
            return this;
        }

        public DatePickerBuilder WeekStart(DayOfWeek weekStart)
        {
            _component.WeekStart = weekStart;
            return this;
        }

        public DatePickerBuilder StartDate(DateTime startDate)
        {
            _component.StartDate = startDate;
            return this;
        }

        public DatePickerBuilder EndDate(DateTime endDate)
        {
            _component.EndDate = endDate;
            return this;
        }

        public DatePickerBuilder ValidDateRange(DateTime startDate, DateTime endDate)
        {
            _component.StartDate = startDate;
            _component.EndDate = endDate;
            return this;
        }

        public DatePickerBuilder TodayButton()
        {
            _component.TodayButton = true;
            return this;
        }

        public DatePickerBuilder TodayButton(bool todayButton)
        {
            _component.TodayButton = todayButton;
            return this;
        }

        public DatePickerBuilder ClearButton()
        {
            _component.ClearButton = true;
            return this;
        }

        public DatePickerBuilder ClearButton(bool clearButton)
        {
            _component.ClearButton = clearButton;
            return this;
        }

        public DatePickerBuilder NameAsPlaceholder()
        {
            _component.NameAsPlaceholder = true;
            return this;
        }

        public DatePickerBuilder NameAsPlaceholder(bool nameAsPlaceholder)
        {
            _component.NameAsPlaceholder = nameAsPlaceholder;
            return this;
        }

        public DatePickerBuilder TodayHighlight()
        {
            _component.TodayHighlight = true;
            return this;
        }

        public DatePickerBuilder TodayHighlight(bool todayHighlight)
        {
            _component.TodayHighlight = todayHighlight;
            return this;
        }

        public DatePickerBuilder ToggleActive()
        {
            _component.ToggleActive = true;
            return this;
        }

        public DatePickerBuilder ToggleActive(bool toggleActive)
        {
            _component.ToggleActive = toggleActive;
            return this;
        }

        public DatePickerBuilder StartView(DPStartView startView)
        {
            _component.StartView = startView;
            return this;
        }

        #endregion

        #region Output

        public string ToHtmlString()
        {
            // make it shorter
            var c = _component;
            var i = Input;
            if (c.NameAsPlaceholder)
            {
                i = i.Replace("{placeholder}", c.DisplayName);
            }
            else
            {
                i = i.Replace("{placeholder}", c.Format);
            }

            if (Value.HasValue)
            {
                i = i.Replace("{value}", Value.Value.ToString(c.Format.Replace('m', 'M')));
            }

            StringBuilder output = new StringBuilder();
            if (c.AsComponent)
            {
                output.Append("<div class=\"input-group date\">");
                output.Append(i);
                output.Append("<span class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></span>");
                output.Append("</div>");
                output.AppendLine();
                output.AppendFormat("<script>$(document).ready(function () {{ $('#{0}').closest('.input-group').datepicker({{", c.Name);
            }
            else
            {
                output.AppendLine(i);
                output.AppendFormat("<script>$(document).ready(function () {{ $('#{0}').datepicker({{", c.Name);
            }

            output.AppendFormat(" orientation: '{0}', ", BsHelper.SplitCamelCase(c.Orientation.ToString()).ToLower());
            output.AppendFormat("format: '{0}', ", c.Format);
            if (c.WeekStart != DayOfWeek.Sunday)
            {
                output.AppendFormat("weekStart: {0}", (int)c.WeekStart);
            }
            if (c.StartDate != DateTime.MinValue)
            {
                output.AppendFormat("startDate: '{0}', ", c.StartDate.ToString("MM/dd/yyyy"));
            }
            if (c.EndDate != DateTime.MaxValue)
            {
                output.AppendFormat("endDate: '{0}', ", c.EndDate.ToString("MM/dd/yyyy"));
            }

            if (c.StartView != DPStartView.Month)
            {
                output.AppendFormat("startView: {0}, ", (int)c.StartView);
            }

            if (c.TodayButton)
                output.Append("todayBtn: 'linked', ");

            if (c.ClearButton)
                output.Append("clearBtn: true, ");

            if (c.TodayHighlight)
                output.Append("todayHighlight: true, ");

            if (c.ToggleActive)
                output.Append("toggleActive: true, ");

            output.AppendLine("}); })</script>");

            return output.ToString();
        }

        #endregion
    }
}
