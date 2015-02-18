using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSiteMapProvider_383.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult ProductDetails(string productType)
        {
            if (string.IsNullOrEmpty(productType))
            {
                return View("Index");
            }
            else
            {
                Session["productType"] = productType;

                return View();
            }
        }

        public ActionResult ChooseType()
        {
            var productType = Convert.ToString(Session["productType"]);
            ViewBag.ProductType = productType;

            return View();
        }
    }
}
