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

            newProducts = allProducts.Where(en => en.isnew).ToList();
            hotProducts = allProducts.Where(en => en.ishot).ToList();

            newProducts = newProducts.Count > 6 ? newProducts.Take(6).ToList(): newProducts;
            hotProducts = hotProducts.Count > 6 ? hotProducts.Take(6).ToList() : hotProducts;

            ViewData["hotProducts"] = hotProducts;
            ViewData["newProducts"] = newProducts;
            SEO seo = SEOService.GetByCode("Home");
            ViewBag.Title = seo != null ? seo.seotitle : "";
            ViewBag.Keywords = seo != null ? seo.seokeywords : "";
            ViewBag.Description = seo != null ? seo.seodescription : "";
            
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