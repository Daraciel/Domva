using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace domvaproject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "ASP.NET MVC";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
