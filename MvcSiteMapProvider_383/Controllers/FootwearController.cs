using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSiteMapProvider_383.Controllers
{
    public class FootwearController : Controller
    {
        public ActionResult MensFootwear()
        {
            return View();
        }

        public ActionResult WomensFootwear()
        {
            return View();
        }

        public ActionResult KidsFootwear()
        {
            return View();
        }

        public ActionResult SearchList(string query)
        {
            return View();
        }

    }
}
