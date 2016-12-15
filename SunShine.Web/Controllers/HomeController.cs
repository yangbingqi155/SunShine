using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunShine.EF;
using SunShine.Model;
using SunShine.BLL;

namespace SunShine.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {

            List<ProductViewModel> hotProducts = new List<ProductViewModel>();
            List<ProductViewModel> newProducts = new List<ProductViewModel>();
            List<ProductViewModel> allProducts = new List<ProductViewModel>();
            allProducts = ProductService.GetALLViewModels();

            newProducts = allProducts.Count > 10 ? allProducts.Take(10).ToList(): allProducts;
            hotProducts = newProducts;

            ViewData["hotProducts"] = hotProducts;
            ViewData["newProducts"] = newProducts;

            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}