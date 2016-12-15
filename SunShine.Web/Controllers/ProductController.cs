using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunShine.EF;
using SunShine.Model;
using SunShine.BLL;
using SunShine.Utils;

namespace SunShine.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List(string idproduct="",string idcategory="",string categoryCode="", int pageIndex = 0)
        {
            int pageCount = 0;
            int pageSize = 2;
            bool result = true;
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            if (!string.IsNullOrEmpty(idproduct)&& result) {
                productViewModels.Add(ProductService.GetViewModel(idproduct));
                result = false;
            }
            if (!string.IsNullOrEmpty(idcategory) && result) {
                productViewModels = ProductService.GetViewModelsByCategory(idcategory);
                result = false;
            }
            if (!string.IsNullOrEmpty(categoryCode) && result) {
                productViewModels = ProductService.GetViewModelsByCategoryCode(categoryCode);
                result = false;
            }
            if (result) {
                productViewModels = ProductService.GetALLViewModels();
            }

            List<ProductViewModel> pageList = productViewModels.Pager<ProductViewModel>(pageIndex, pageSize, out pageCount);

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;

            RouteData.Values.Add("idproduct", idproduct);
            RouteData.Values.Add("idcategory", idcategory);
            RouteData.Values.Add("categoryCode", categoryCode);
            
            return View(pageList);
        }
    }
}