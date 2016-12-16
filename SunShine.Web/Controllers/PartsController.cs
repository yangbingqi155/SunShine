using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunShine.Model;
using SunShine.BLL;
using SunShine.EF;

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

        /// <summary>
        /// 导航-成功案例
        /// </summary>
        /// <returns></returns>
        public ActionResult NavSuccessCases(string categoryCode) {
            return View(SiteCategoryService.GetChildCategoriesByCode(categoryCode));
        }

        /// <summary>
        /// 导航-荣誉资质
        /// </summary>
        /// <returns></returns>
        public ActionResult NavHonors() {
            string categoryCode = "honor";
            return View(ArticleService.GetArticlesByCategoryCode(categoryCode));
        }

        /// <summary>
        /// 导航-联系我们
        /// </summary>
        /// <returns></returns>
        public ActionResult NavContactUs() {
            return View();
        }

        /// <summary>
        /// 底部产品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult BottomProductCategory() {
            List<ProductCategoryViewModel> categories = new List<ProductCategoryViewModel>();
            List<ProductCategory> tempCategories = ProductCategoryService.GetALL();
            categories = tempCategories.Select(model=> {
                ProductCategoryViewModel category = new ProductCategoryViewModel();
                category.CopyFromBase(model);
                return category;
            }).ToList();
            return View(categories);
        }

        /// <summary>
        /// 左边产品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult LeftProductCategory() {
            List<ProductCategoryViewModel> categories = new List<ProductCategoryViewModel>();
            List<ProductCategory> tempCategories = ProductCategoryService.GetALL();
            categories = tempCategories.Select(model => {
                ProductCategoryViewModel category = new ProductCategoryViewModel();
                category.CopyFromBase(model);
                return category;
            }).ToList();
            return View(categories);
        }

        /// <summary>
        /// 合作伙伴
        /// </summary>
        /// <returns></returns>
        public ActionResult CaseListItemsScroll() {
            string categoryCode = "partner";
            return View(ArticleService.GetArticlesByCategoryCode(categoryCode));
        }
    }
}