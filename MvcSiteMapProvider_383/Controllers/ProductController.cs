using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSiteMapProvider_383.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetails(string productType)
        {
            Session["productType"] = productType;

            return View();
        }

        public ActionResult ChooseType()
        {
            var productType = Convert.ToString(Session["productType"]);
            ViewBag.ProductType = productType;

            return View();
        }

        //[HttpPost]
        //public ActionResult ChooseType(string type)
        //{
        //    var productType = Convert.ToString(Session["productType"]);

        //    return new RedirectResult(Url.Action(type + productType));
        //}

    }
}
