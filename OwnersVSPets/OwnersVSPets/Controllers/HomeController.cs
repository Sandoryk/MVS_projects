using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OwnersVSPets.DAL;

namespace OwnersVSPets.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = "OwnersPetsConnection";
        DataSourceHandler dh;
        public ActionResult Owners()
        {
 
            return View();
        }
        public ActionResult Pets(int? ownerID)
        {
            int ID = ownerID == null ? -1 : (int)ownerID;

            ViewBag.ID = ID;
            ViewBag.OwnerName = "";
            if (ID>-1)
            {
                using (dh = new DataSourceHandler(connectionString))
	            {
                    DBOwner owner = dh.GetOwnerByID(ID);
                    if (owner!=null)
                    {
                        ViewBag.OwnerName = owner.Name;
                    }
	            } 
            }

            return View(ID);
        }
    }
}
