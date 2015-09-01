using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Mvc.Models;

namespace Test.Mvc.Controllers
{
    public class InputController : Controller
    {
        public ActionResult Summernote()
        {
            return View();
        }

        public ActionResult CKEditor()
        {
            return View();
        }

        public ActionResult DatePicker()
        {
            PersonModel model = new PersonModel();
            return View(model);
        }

        public ActionResult Select2()
        {
            FormModel model = new FormModel();
            return View(model);
        }
    }
}