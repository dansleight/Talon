using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.Mvc.Models
{
    public class FormModel
    {
        #region Properties

        public string StateCode { get; set; }

        #endregion

        #region Lists

        public SelectList StatesList()
        {
            return new SelectList(StateModel.GetStates(), "Abbreviation", "State");
        }

        #endregion
    }
}