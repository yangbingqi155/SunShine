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
using SunShine.Web.Utils;

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



        /// <summary>
        /// 产品列表
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult ProductList(int pageIndex = 0,string name = "", string idusagecategory = "", string idindustrycategory = "", string idproductcategory = "")
        {
            int pageCount = 0;
            int pageSize = 10;
            List<ProductViewModel> entities = ProductService.SearchViewModels(name,idusagecategory,idindustrycategory,idproductcategory);
            List<ProductViewModel> pageList = entities.Pager<ProductViewModel>(pageIndex, pageSize, out pageCount);
            
            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;

            RouteData.Values.Add("name", name);
            RouteData.Values.Add("idusagecategory", idusagecategory);
            RouteData.Values.Add("idindustrycategory", idindustrycategory);
            RouteData.Values.Add("idproductcategory", idproductcategory);

            ViewData["name"] = name;
            ViewData["idusagecategory"] = idusagecategory;
            ViewData["idindustrycategory"] = idindustrycategory;
            ViewData["idproductcategory"] = idproductcategory;

            return View(entities);
        }


        /// <summary>
        /// 启用或者禁用产品
        /// </summary>
        /// <param name="idproduct"></param>
        /// <param name="enable"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [HttpPost]
        [ManageLoginValidation]
        public ActionResult ProductEnable(string idproduct, bool enable, bool isAjax)
        {
            ResultModel<ProductViewModel> resultEntity = new ResultModel<ProductViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try
            {
                Product product = ProductService.Get(idproduct);
                product.inuse = enable;
                ProductService.Edit(product);
            }
            catch (Exception ex)
            {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = ex.ToString();
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 新增\编辑产品
        /// </summary>
        /// <param name="idproduct"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult ProductEdit(string idproduct = "")
        {
            ProductViewModel model = new ProductViewModel();
            if (!string.IsNullOrEmpty(idproduct))
            {
                Product product = ProductService.Get(idproduct);
                if (product != null) { model.CopyFromBase(product); }
            }
            else
            {
                model.inuse = true;
            }

            return View(model);
        }

        /// <summary>
        /// 新增\编辑产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProductEdit(ProductViewModel model, string productImages = "")
        {
            Product product = new Product();
            model.CopyToBase(product);
            if (string.IsNullOrEmpty(product.idproduct))
            {
                product.idproduct = Pub.ID();
                product.cretime = DateTime.Now;
                //新增
                product = ProductService.Add(product);
            }
            else
            {
                //编辑
                product = ProductService.Edit(product);
            }

            //修改后重新加载
            model.CopyFromBase(product);

            if (!string.IsNullOrEmpty(productImages))
            {
                List<Picture> list = new List<Picture>();
                string[] imgs = productImages.Split(',');
                int i = 0;
                foreach (var item in imgs)
                {
                    list.Add(new Picture()
                    {
                        idimage = Pub.ID(),
                        idmodule = product.idproduct,
                         moduletype=(int)ModuleType.Product,
                        path = item,
                        sortno = i + 1,
                        cretime=DateTime.Now
                    });
                    i++;
                }
                if (list.Count > 0)
                {
                    PictureService.AddMuti(list);
                }
            }

            ProductService.SetDefaultImage(product.idproduct);

            ModelState.AddModelError("", "保存成功.");

            return View(model);
        }


        /// <summary>
        /// 网站类别列表
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult SiteCategoryList(int pageIndex = 0)
        {
            int pageCount = 0;
            int pageSize = 10;
            List<SiteCategory> entities = SiteCategoryService.GetALL();
            List<SiteCategory> pageList = entities.Pager<SiteCategory>(pageIndex, pageSize, out pageCount);


            List<SiteCategoryViewModel> viewModels = pageList.Select(model => {
                SiteCategoryViewModel viewModel = new SiteCategoryViewModel();
                viewModel.CopyFromBase(model);
                return viewModel;
            }).ToList();

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;

            return View(viewModels);
        }


        /// <summary>
        /// 启用或者禁用网站类别
        /// </summary>
        /// <param name="idcategory"></param>
        /// <param name="enable"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [HttpPost]
        [ManageLoginValidation]
        public ActionResult SiteCategoryEnable(string idcategory, bool enable, bool isAjax)
        {
            ResultModel<SiteCategoryViewModel> resultEntity = new ResultModel<SiteCategoryViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try
            {
                SiteCategory category = SiteCategoryService.Get(idcategory);
                category.inuse = enable;
                SiteCategoryService.Edit(category);
            }
            catch (Exception ex)
            {
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
        public ActionResult SiteCategoryEdit(string idcategory = "")
        {
            SiteCategoryViewModel model = new SiteCategoryViewModel();
            if (!string.IsNullOrEmpty(idcategory))
            {
                SiteCategory category = SiteCategoryService.Get(idcategory);
                if (category != null) { model.CopyFromBase(category); }
            }
            else
            {
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
        public ActionResult SiteCategoryEdit(SiteCategoryViewModel model)
        {
            SiteCategory category = new SiteCategory();
            model.CopyToBase(category);
            if (string.IsNullOrEmpty(category.idcategory))
            {
                category.idcategory = Pub.ID();
                category.cretime = DateTime.Now;
                //新增
                category = SiteCategoryService.Add(category);
            }
            else
            {
                //编辑
                category = SiteCategoryService.Edit(category);
            }

            //修改后重新加载
            model.CopyFromBase(category);

            ModelState.AddModelError("", "保存成功.");

            return RedirectToAction("SiteCategoryList", "Manage");
        }


        [ManageLoginValidation]
        public ActionResult UploadImage(ModuleType moduleType,string idmodule="")
        {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            ResultModel<PictureViewModel> resultEntity = new ResultModel<PictureViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "文件上传成功";
            string GUID = System.Guid.NewGuid().ToString();
            string path = "~/Resource/Images/";
            if (moduleType== ModuleType.Product) {
                path += "Product/";
            }
            string filename = string.Empty;
            string message = string.Empty;
            try
            {
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    //if (Request.Files.Count > 1)
                    //{
                    //    resultEntity.Code = ResponseCodeType.Fail;
                    //    resultEntity.Message = "请选择文件.";
                    //    return Content(resultEntity.SerializeToJson());
                    //}
                    resultEntity.Content = new List<PictureViewModel>();
                    int i = 0;
                    foreach (string upload in Request.Files)
                    {
                        GUID = System.Guid.NewGuid().ToString();
                        if (!Request.Files[i].HasFile())
                        {
                            resultEntity.Code = ResponseCodeType.Fail;
                            resultEntity.Message = "文件大小不能0.";
                            return Content(resultEntity.SerializeToJson());
                        }


                        if (!CheckFileType((HttpPostedFileWrapper)((HttpFileCollectionWrapper)Request.Files)[i]))
                        {
                            resultEntity.Code = ResponseCodeType.Fail;
                            resultEntity.Message = "文件类型只能是jpg,bmp,gif,PNG..";
                            return Content(resultEntity.SerializeToJson());
                        }

                        //获取文件后缀名
                        string originFileName = Request.Files[i].FileName;
                        string originFileNameSuffix = string.Empty;
                        int lastIndex = originFileName.LastIndexOf(".");
                        if (lastIndex < 0)
                        {
                            resultEntity.Code = ResponseCodeType.Fail;
                            resultEntity.Message = "文件类型只能是jpg,bmp,gif,PNG..";
                            return Content(resultEntity.SerializeToJson());
                        }
                        originFileNameSuffix = originFileName.Substring(lastIndex, originFileName.Length - lastIndex);

                        filename = GUID + originFileNameSuffix;
                        if (!Directory.Exists(Server.MapPath(path)))
                        {
                            Directory.CreateDirectory(Server.MapPath(path));
                        }

                        Request.Files[i].SaveAs(Path.Combine(Server.MapPath(path), filename));
                        PictureViewModel model = new PictureViewModel()
                        {
                            idmodule = idmodule,
                             path = path + filename,
                            sortno = 0
                        };
                        if (moduleType == ModuleType.Product)
                        {
                            model.sortno = ProductService.MaxProductImageSort(ModuleType.Product,idmodule);
                        }

                        resultEntity.Content.Add(model);
                        i++;
                        StringBuilder initialPreview = new StringBuilder();
                        StringBuilder initialPreviewConfig = new StringBuilder();
                        initialPreviewConfig.Append(",\"initialPreviewConfig\":[");
                        initialPreview.Append("{\"initialPreview\":[");
                        for (int k = 0; k < resultEntity.Content.Count; k++)
                        {
                            if (k == 0)
                            {
                                initialPreview.AppendFormat("\"{0}\"", Url.Content(resultEntity.Content[k].path));
                                initialPreviewConfig.Append("{\"url\":\"" + Url.Action("DeleteImage", "Manage") + "\"}");
                            }
                            else
                            {
                                initialPreview.AppendFormat("\",{0}\"", Url.Content(resultEntity.Content[k].path));
                                initialPreviewConfig.Append(",{\"url\":\"" + Url.Action("DeleteImage", "Manage") + "\"}");
                            }
                        }
                        initialPreview.Append("]");
                        initialPreviewConfig.Append("]");
                        initialPreview.Append(initialPreviewConfig.ToString());
                        initialPreview.Append("}");
                        return Content(initialPreview.ToString());
                    }

                }
                else
                {
                    resultEntity.Code = ResponseCodeType.Fail;
                    resultEntity.Message = "文件上传失败.";
                    return Content(resultEntity.SerializeToJson());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = "没有选择要上传的文件.";
                return Content(resultEntity.SerializeToJson());
            }
            return Content(resultEntity.SerializeToJson());


        }


        public static bool DeleteImages(string idmodule,ModuleType moduleType)
        {
            bool result = false;
            try
            {
                TN db = new TN();
                db.Pictures.RemoveRange(db.Pictures.Where(en => en.idmodule == idmodule && en.moduletype==(int)moduleType));
                db.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="idmodule"></param>
        /// <param name="moduleType"></param>
        /// <param name="isAjax"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult AjaxImageList(string idmodule,ModuleType moduleType, bool isAjax)
        {
            ResultModel<PictureViewModel> resultEntity = new ResultModel<PictureViewModel>();
            resultEntity.Code = ResponseCodeType.Success;
            resultEntity.Message = "成功";
            try
            {
                List<Picture> entities = PictureService.GetImagesByIdmodule(idmodule, moduleType);
                List<PictureViewModel> viewModels = entities.Select(model =>
                {
                    PictureViewModel viewModel = new PictureViewModel();
                    viewModel.CopyFromBase(model);
                    viewModel.path = Url.Content(viewModel.path);
                    return viewModel;
                }).ToList();
                resultEntity.Content = viewModels;
            }
            catch (Exception ex)
            {
                resultEntity.Code = ResponseCodeType.Fail;
                resultEntity.Message = ex.ToString();
            }

            return Content(resultEntity.SerializeToJson());
        }

        /// <summary>
        /// 判断上传文件类型
        /// </summary>
        private bool CheckFileType(HttpPostedFileWrapper postedFile)
        {

            bool result = true;
            /*  文件扩展名说明  
            jpg：255216  
            bmp：6677  
            gif：7173  
            PNG：13780
            pdf：3780  
            */
            int[] fileTypes = new int[] { 255216, 6677, 7173, 13780, 3780 };
            string fileTypeStr = "255216, 6677, 7173, 13780, 3780";
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            int fileLength = postedFile.ContentLength;
            if (fileLength <= 0)
            {
                return false;
            }
            byte[] imgArray = new byte[fileLength];

            postedFile.InputStream.Read(imgArray, 0, fileLength);

            System.IO.MemoryStream fs = new System.IO.MemoryStream();
            fs = new System.IO.MemoryStream(imgArray);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = string.Empty;
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch (Exception)
            {
                result = false;
                //Log.Error(ex.ToString());
            }
            finally
            {
                r.Close();
                fs.Close();
                r.Dispose();
                fs.Dispose();
            }
            if (fileTypeStr.IndexOf(fileclass) < 0)
            {
                result = false;
            }

            return result;

        }
    }
}
