using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.IO;
using log4net;
using System.Reflection;
using System;
using System.Text;
using SunShine.Model;
using SunShine.BLL;
using SunShine.EF;
using SunShine.Utils;
using SunShine.Web.Authorize;

namespace TNet.Controllers
{
    public class ManageController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ManageUserViewModel model)
        {
            ManageUser user = ManageUserService.GetManageUserByUserName(model.username);
            if (user == null)
            {
                ModelState.AddModelError("", "没有找到该用户名或者帐号被禁用.");
                return View(model);
            }
            string md5Password = string.Empty;
            md5Password = Crypto.GetPwdhash(model.ClearPassword, user.md5salt);
            if (md5Password.ToUpper() != user.password.ToUpper())
            {
                ModelState.AddModelError("", "密码不正确.");
                return View(model);
            }
            Session["ManageUser"] = user;
            return RedirectToAction("Index", "Manage");
        }

        [ManageLoginValidation]
        public ActionResult SignOut()
        {
            Session["ManageUser"] = null;
            return RedirectToAction("Login", "Manage");
        }

        [ManageLoginValidation]
        public ActionResult Index() {
            return View();
        }


        /// <summary>
        /// 产品类别列表
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult ProductCategoryList(int pageIndex = 0) {
            int pageCount = 0;
            int pageSize = 10;
            List<ProductCategory> entities = ProductCategoryService.GetALL();
            List<ProductCategory> pageList = entities.Pager<ProductCategory>(pageIndex, pageSize, out pageCount);


            List<ProductCategoryViewModel> viewModels = pageList.Select(model => {
                ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
                viewModel.CopyFromBase(model);
                return viewModel;
            }).ToList();

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;

            return View(viewModels);
        }


        /// <summary>
        /// 启用或者禁用产品类别
        /// </summary>
        /// <param name="idcategory"></param>
        /// <param name="enable"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [HttpPost]
        [ManageLoginValidation]
        public ActionResult ProductCategoryEnable(string idcategory, bool enable, bool isAjax) {
            ResultModel<ProductCategoryViewModel> resultEntity = new ResultModel<ProductCategoryViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try {
                ProductCategory category = ProductCategoryService.Get(idcategory);
                category.inuse = enable;
                ProductCategoryService.Edit(category);
            }
            catch (Exception ex) {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = ex.ToString();
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 新增\编辑产品类别
        /// </summary>
        /// <param name="idcategory"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult ProductCategoryEdit(string idcategory = "") {
            ProductCategoryViewModel model = new ProductCategoryViewModel();
            if (!string.IsNullOrEmpty(idcategory)) {
                ProductCategory category = ProductCategoryService.Get(idcategory);
                if (category != null) { model.CopyFromBase(category); }
            }
            else {
                model.inuse = true;
            }

            return View(model);
        }

        /// <summary>
        /// 新增\编辑产品类别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult ProductCategoryEdit(ProductCategoryViewModel model) {
            ProductCategory category = new ProductCategory();
            model.CopyToBase(category);
            if (string.IsNullOrEmpty(category.idcategory)) {
                category.idcategory = Pub.ID();
                category.cretime = DateTime.Now;
                //新增
                category = ProductCategoryService.Add(category);
            }
            else {
                //编辑
                category = ProductCategoryService.Edit(category);
            }

            //修改后重新加载
            model.CopyFromBase(category);

            ModelState.AddModelError("", "保存成功.");

            return View(model);
        }
    }
}
