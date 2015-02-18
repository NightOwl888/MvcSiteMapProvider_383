using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSiteMapProvider_383.Controllers
{
    public class ClothingController : Controller
    {
        public ActionResult MensClothing()
        {
            return View();
        }

        public ActionResult WomensClothing()
        {
            return View();
        }

        public ActionResult KidsClothing()
        {
            return View();
        }

        public ActionResult SearchList(string query)
        {
            return View();
        }
    }
}
