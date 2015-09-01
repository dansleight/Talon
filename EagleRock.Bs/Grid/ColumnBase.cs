using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Bs.Grid
{
    public class ColumnBase<T> where T : class
    {
        #region Properties

        public Grid<T> Grid { get; private set; }

        public virtual string Template { get; set; }

        internal ColumnSettings Settings { get; set; }

        public ColumnType ColumnType { get; private set; }

        public string Member
        {
            get { return this.Settings.Member; }
            set { this.Settings.Member = value; }
        }

        public string Title
        {
            get { return this.Settings.Title; }
            set { this.Settings.Title = value; }
        }

        public string Format
        {
            get { return this.Settings.Format; }
            set { this.Settings.Format = value; }
        }

        public bool Hidden
        {
            get { return this.Settings.Hidden; }
            set { this.Settings.Hidden = value; }
        }

        public int Width
        {
            get { return this.Settings.Width; }
            set { this.Settings.Width = value; }
        }

        public bool Sortable
        {
            get { return this.Settings.Sortable; }
            set { this.Settings.Sortable = value; }
        }

        public int SortOrder
        {
            get { return this.Settings.SortOrder; }
            set { this.Settings.SortOrder = value; }
        }

        public SortDir SortDirection
        {
            get { return this.Settings.SortDirection; }
            set { this.Settings.SortDirection = value; }
        }

        public string ClassName {
            get { return this.Settings.ClassName; }
            set { this.Settings.ClassName = value; }
        }

        public bool Searchable
        {
            get { return this.Settings.Searchable; }
            set { this.Settings.Searchable = value; }
        }

        public ColumnResponsiveAction? ResponsiveAction
        {
            get { return this.Settings.ResponsiveAction; }
            set { this.Settings.ResponsiveAction = value; }
        }

        public ColumnResponsiveSize? ResponsiveSize
        {
            get { return this.Settings.ResponsieSize; }
            set { this.Settings.ResponsieSize = value; }
        }

        public string Render
        {
            get { return this.Settings.Render; }
            set { this.Settings.Render = value; }
        }

        internal int Index { get; set; }

        internal Type Type { get; set; }

        #endregion

        #region Constructor

        protected ColumnBase(Grid<T> grid, ColumnType columnType)
        {
            this.Grid = grid;
            this.ColumnType = columnType;
            this.Settings = new ColumnSettings();
            this.Searchable = true;
        }

        #endregion

        #region Methods

        public void ProcessResponsive()
        {
            if (this.ClassName == null) this.ClassName = String.Empty;
            if (this.ResponsiveAction.HasValue)
            {
                string prefix = " hidden-";
                if (this.ResponsiveAction.Value == ColumnResponsiveAction.Show)
                    prefix = " visible-";
                foreach (ColumnResponsiveSize s in Enum.GetValues(typeof(ColumnResponsiveSize)))
                {
                    this.ClassName += prefix + s.ToString();
                    if (this.ResponsiveSize == s)
                        break;
                }
            }
        }

        #endregion

    }
}
