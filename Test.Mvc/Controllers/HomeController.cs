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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AsJs()
        {
            return View(Models.StateModel.GetStates());
        }

        public ActionResult AsTalon()
        {
            return View(Models.StateModel.GetStates());
        }

        public ActionResult AsTalonAjax()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public JsonResult ServerSideData(GridDataRequestObject<StateModel> req)
        {
            //if (req.HasCache("states"))
            //    return Json(req.GetResultFromCache());
            //return Json(req.GetResultAndCache(Models.StateModel.GetStates(), "states", 120));
            return Json(req.GetResult(Models.StateModel.GetStates()));
        }
    }
}