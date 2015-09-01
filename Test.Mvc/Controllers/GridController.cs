using EagleRock.Bs;
using EagleRock.Bs.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Mvc.Models;

namespace Test.Mvc.Controllers
{
    public class GridController : Controller
    {
        // GET: Grid
        public ActionResult Index()
        {
            // basic grid
            List<PersonModel> model = PersonModel.GetPeople();
            return View(model);
        }

        public ActionResult Responsive()
        {
            List<PersonModel> model = PersonModel.GetPeople();
            return View(model);
        }

        public ActionResult PageSort()
        {
            List<PersonModel> model = PersonModel.GetPeople();
            return View(model);
        }

        public ActionResult Scrolling()
        {
            List<PersonModel> model = PersonModel.GetPeople();
            return View(model);
        }

        public ActionResult Formatting()
        {
            List<PersonModel> model = PersonModel.GetPeople();
            return View(model);
        }

        public ActionResult Custom()
        {
            List<PersonModel> model = PersonModel.GetPeople();
            return View(model);
        }

        public ActionResult Render()
        {
            List<PersonModel> model = PersonModel.GetPeople();
            return View(model);
        }

        public ActionResult Buttons()
        {
            List<PersonModel> model = PersonModel.GetPeople();
            return View(model);
        }

        public ActionResult ServerSide()
        {
            return View();
        }

        public JsonResult ServerSideData(GridDataRequestObject<PersonModel> req)
        {
            if (req.HasCache("people"))
                return Json(req.GetResultFromCache());
            return Json(req.GetResultAndCache(Models.PersonModel.GetPeople(), "people", 120));
        }
    }
}