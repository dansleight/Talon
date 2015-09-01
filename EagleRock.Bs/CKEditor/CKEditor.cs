using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace EagleRock.Bs.CKEditor
{
    public class CKEditor
    {
        #region Properties

        public string Name { get; set; }

        public int Height { get; set; }

        public bool Focus { get; set; }

        public string Value { get; set; }

        #endregion

        #region Constructor

        public CKEditor (ViewContext viewContext, string name)
        {
            this.Name = name;
            this.Focus = false;
        }

        #endregion

        #region Object Methods

        protected void WriteHtml(HtmlTextWriter writer)
        {
            writer.WriteLine("<textarea name=\"{0}\" id=\"{0}\">{1}</textarea>", Name, Value);
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            writer.WriteLine("$(document).ready(function() {");
            writer.WriteLine("CKEDITOR.replace({0}, {{", Name);
            if (Height > 0)
            {
                writer.WriteLine("    height: {0},", Height);
            }
            if (Focus)
            {
                writer.WriteLine(" on: {{ 'instanceReady': function(e) {{ CKEDITOR.instances.{0}.focus() }} }},", Name);
            }
            writer.WriteLine("})");

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
