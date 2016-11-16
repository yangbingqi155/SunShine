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

        
    }
}
