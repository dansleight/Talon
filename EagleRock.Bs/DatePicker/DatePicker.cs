using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EagleRock.Bs.DatePicker
{
    public class DatePicker
    {
        #region Properties

        public string Name { get; set; }

        private string displayName;
        public string DisplayName {
            get
            {
                if (!String.IsNullOrEmpty(displayName))
                    return displayName;
                return BsHelper.SplitCamelCase(Name);
            }
            set
            {
                displayName = value;
            }
        }
        public bool AsComponent { get; set; }
        public DPOrientation Orientation { get; set; }
        public string Format { get; set; }
        public DayOfWeek WeekStart { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DPStartView StartView { get; set; }
        public bool TodayButton { get; set; }
        public bool ClearButton { get; set; }
        public bool NameAsPlaceholder { get; set; }
        public bool TodayHighlight { get; set; }
        public bool ToggleActive { get; set; }


        #endregion

        #region Constructor

        public DatePicker(string name)
        {
            Name = name;
            AsComponent = true;
            Orientation = DPOrientation.TopAuto;
            Format = "m/d/yyyy";
            WeekStart = DayOfWeek.Sunday;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue;
            StartView = DPStartView.Month;
            TodayButton = false;
            ClearButton = false;
            NameAsPlaceholder = false;
            TodayHighlight = false;
            ToggleActive = false;
        }

        #endregion
    }

    public enum DPOrientation
    {
        TopAuto,
        Auto,
        BottomAuto,
        AutoLeft,
        TopLeft,
        BottomLeft,
        AutoRight,
        TopRight,
        BottomRight
    }

    public enum DPStartView
    {
        Month = 0,
        Year = 1,
        Decade = 2
    }
}

