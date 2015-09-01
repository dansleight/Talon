using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EagleRock.Bs.Summernote
{
    public class SummernoteBuilder : IHtmlString
    {
        #region Properties

        private Summernote _component;
        protected internal Summernote Component
        {
            get { return this._component; }
            set { this._component = value; }
        }

        #endregion

        #region Constructor

        public SummernoteBuilder(Summernote component)
        {
            this.Component = component;
        }

        #endregion

        #region Object Methods

        public SummernoteBuilder Height(int height)
        {
            this.Component.Height = height;
            return this;
        }

        public SummernoteBuilder MinHeight(int minheight)
        {
            this.Component.MinHeight = minheight;
            return this;
        }

        public SummernoteBuilder MaxHeight(int maxheight)
        {
            this.Component.MaxHeight = maxheight;
            return this;
        }

        public SummernoteBuilder Focus()
        {
            return Focus(true);
        }

        public SummernoteBuilder Focus(bool focus)
        {
            this.Component.Focus = focus;
            return this;
        }

        public SummernoteBuilder Value(string value)
        {
            this.Component.Value = value;
            return this;
        }

        public string ToHtmlString()
        {
            return this.Component.ToHtmlString();
        }

        #endregion
    }
}
