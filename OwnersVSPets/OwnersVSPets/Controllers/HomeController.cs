using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OwnersVSPets.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Pets' owners";

            return View();
        }
        public ActionResult Pets()
        {
            ViewBag.Title = "Pets";

            return View();
        }
    }
}
