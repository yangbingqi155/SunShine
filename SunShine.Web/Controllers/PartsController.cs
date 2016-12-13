using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShine.Web.Controllers
{
    public class PartsController : Controller
    {
        // GET: HotSale
        public ActionResult HotSale()
        {
            return View();
        }

        /// <summary>
        /// 合作伙伴
        /// </summary>
        /// <returns></returns>
        public ActionResult Partner() {
            return View();
        }
    }
}