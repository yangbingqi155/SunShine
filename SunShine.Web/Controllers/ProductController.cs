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
            string contactusStr = string.Empty;
            int pageCount = 0;
            int pageSize = 6;
            bool result = true;
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            ProductCategory category = null;
            if (!string.IsNullOrEmpty(idproduct)&& result) {
                productViewModels.Add(ProductService.GetViewModel(idproduct));
                category = productViewModels.Count>0? productViewModels.First().ParentCategory:null;
                List<ArticleViewModel> contactus= ArticleService.GetArticlesByCategoryCode("contactus");
                contactusStr = contactus.Count > 0 ? contactus.First().content: " <div class=\"no_data_div\">暂无联系方式！</div>";
                result = false;
            }
            if (!string.IsNullOrEmpty(idcategory) && result) {
                productViewModels = ProductService.GetViewModelsByCategory(idcategory).Where(en=>en.inuse).ToList();
                category = ProductCategoryService.Get(idcategory);
                result = false;
            }
            if (!string.IsNullOrEmpty(categoryCode) && result) {
                productViewModels = ProductService.GetViewModelsByCategoryCode(categoryCode).Where(en => en.inuse).ToList();
                category = ProductCategoryService.GetByCategoryCode(categoryCode);
                result = false;
            }
            if (result) {
                productViewModels = ProductService.GetALLViewModels().Where(en => en.inuse).ToList();
            }

            if (result)
            {
                SEO seo = SEOService.GetByCode("Home");
                ViewBag.Title = seo != null ? seo.seotitle : "";
                ViewBag.Keywords = seo != null ? seo.seokeywords : "";
                ViewBag.Description = seo != null ? seo.seodescription : "";
            }
            else
            {
                string productTitle = "";
                if (!string.IsNullOrEmpty(idproduct)) {
                    productTitle = productViewModels.Count > 0 ? "-" + productViewModels.First().name : "";
                }
                ViewBag.Title = "创意阳光-产品中心"+ (string.IsNullOrEmpty(category.categoryname)?"":("-"+ category.categoryname))+ productTitle;
            }

            ViewBag.Keywords = category != null ? category.keyword : "";
            ViewBag.Description = category != null ? category.description : "";

            List<ProductViewModel> pageList = productViewModels.Pager<ProductViewModel>(pageIndex, pageSize, out pageCount);

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["contactus"] = contactusStr;

            RouteData.Values.Add("idproduct", idproduct);
            RouteData.Values.Add("idcategory", idcategory);
            RouteData.Values.Add("categoryCode", categoryCode);
            
            return View(pageList);
        }
    }
}