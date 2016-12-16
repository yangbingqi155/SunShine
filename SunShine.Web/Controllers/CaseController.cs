using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunShine.Model;
using SunShine.BLL;
using SunShine.EF;
using SunShine.Utils;

namespace SunShine.Web.Controllers
{
    public class CaseController : Controller
    {
        // GET: Case
        public ActionResult Index(string categoryCode,string currentCategoryCode="",string idarticle="",int pageIndex = 0)
        {
            int pageCount = 0;
            int pageSize = 10;
            if (string.IsNullOrEmpty(currentCategoryCode)) {
               List<SiteCategoryViewModel> childrenCategories= SiteCategoryService.GetChildCategoriesByCode(categoryCode);
                if (childrenCategories != null && childrenCategories.Count > 0) {
                    currentCategoryCode = childrenCategories.First().categorycode;
                }
                else {
                    return RedirectToAction("Index","Home");
                }
            }

            List<ArticleViewModel> articles = new List<ArticleViewModel>();
            if (!string.IsNullOrEmpty(idarticle))
            {
                articles.Add(ArticleService.GetViewModel(idarticle));
            }
            else
            {
                articles = ArticleService.GetArticlesByCategoryCode(currentCategoryCode);
            }

            List<ArticleViewModel> pageList = articles.Pager<ArticleViewModel>(pageIndex, pageSize, out pageCount);

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["navPath"] = SiteCategoryService.GetNavPath(currentCategoryCode);
            ViewData["currentCategoryCode"] = currentCategoryCode;

            RouteData.Values.Add("categoryCode", categoryCode);
            RouteData.Values.Add("currentCategoryCode", currentCategoryCode);
            return View(pageList);

        }
    }
}