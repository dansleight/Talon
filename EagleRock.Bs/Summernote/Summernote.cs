using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace EagleRock.Bs.Summernote
{
    public class Summernote
    {
        #region Properties

        public string Name { get; set; }

        public int Height { get; set; }

        public int MinHeight { get; set; }

        public int MaxHeight { get; set; }

        public bool Focus { get; set; }

        public string Value { get; set; }

        #endregion

        #region Constructor

        public Summernote (ViewContext viewContext, string name)
        {
            this.Name = name;
            this.Height = 300;
            this.MinHeight = 200;
            this.MaxHeight = 2000;
            this.Focus = false;
        }

        #endregion

        #region Object Methods

        protected void WriteHtml(HtmlTextWriter writer)
        {
            writer.WriteLine(String.Format("<div name=\"{0}\" id=\"{0}\">{1}</div>", Name, Value));
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            writer.WriteLine("$(document).ready(function() {");
            writer.WriteLine("$('#{0}').summernote({{ height: {1}, minHeight: {2}, maxHeight: {3}, focus: {4} }})",
                this.Name,
                this.Height,
                this.MinHeight,
                this.MaxHeight,
                this.Focus.ToString().ToLower());
            writer.WriteLine("});");
            writer.RenderEndTag();
        }

        public string ToHtmlString()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                this.WriteHtml(new HtmlTextWriter((TextWriter)stringWriter));
                return stringWriter.ToString();
            }
        }

        #endregion
    }
}
