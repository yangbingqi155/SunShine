using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShine.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List(string idproduct="",string idcategory="",string categoryCode="")
        {
            return View();
        }
    }
}