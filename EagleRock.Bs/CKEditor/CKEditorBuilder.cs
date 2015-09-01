using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EagleRock.Bs.CKEditor
{
    public class CKEditorBuilder : IHtmlString
    {
        #region Properties

        private CKEditor _component;
        protected internal CKEditor Component
        {
            get { return this._component; }
            set { this._component = value; }
        }

        #endregion

        #region Constructor

        public CKEditorBuilder(CKEditor component)
        {
            this.Component = component;
        }

        #endregion

        #region Object Methods

        public CKEditorBuilder Height(int height)
        {
            this.Component.Height = height;
            return this;
        }

        public CKEditorBuilder Value(string value)
        {
            this.Component.Value = value;
            return this;
        }

        public CKEditorBuilder Focus()
        {
            this.Component.Focus = true;
            return this;
        }

        public string ToHtmlString()
        {
            return this.Component.ToHtmlString();
        }

        #endregion
    }
}
