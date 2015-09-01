using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EagleRock.Bs
{
    public static class HtmlHelperExtension
    {
        public static TalonFactory<TModel> Talon<TModel>(this HtmlHelper<TModel> helper)
        {
            return new TalonFactory<TModel>(helper);
        }
    }
}
