using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SunShine.Model;
using SunShine.EF;

namespace SunShine.BLL
{
    public class ProductService
    {
        public static List<Product> GetALL()
        {
            TN db = new TN();
            return db.Products.OrderBy(en => en.sortno).ToList();
        }

        private static List<ProductViewModel> ConvertToViewModel(List<Product> products) {
            List<ProductViewModel> entities = new List<ProductViewModel>();
            TN db = new TN();
            List<ProductCategory> categories = db.ProductCategories.ToList();
            entities = products.Select(model => {
                ProductViewModel viewModel = new ProductViewModel();
                viewModel.CopyFromBase(model);
                ProductCategory usagecategory = categories.Find(en => model.idusagecategory == en.idcategory);
                ProductCategory industrycategory = categories.Find(en => model.idindustrycategory == en.idcategory);
                ProductCategory productcategory = categories.Find(en => model.idproductcategory == en.idcategory);
                viewModel.usageCategoryName = usagecategory != null ? usagecategory.categoryname : "";
                viewModel.industryCategoryName = industrycategory != null ? industrycategory.categoryname : "";
                viewModel.productCategoryName = productcategory != null ? productcategory.categoryname : "";
                return viewModel;
            }).ToList();
            return entities;
        }

        public static List<ProductViewModel> SearchViewModels(string name = "", string idusagecategory = "", string idindustrycategory = "", string idproductcategory = "") {
            List<Product> list = Search(name,idusagecategory,idindustrycategory,idproductcategory);
            return ConvertToViewModel(list);
        }

        public static List<Product> Search(string name="",string idusagecategory="",string idindustrycategory="",string idproductcategory="") {
            List<Product> list = new List<Product>();
            TN db = new TN();
            list=db.Products.Where(en=> 
            (string.IsNullOrEmpty(idusagecategory)||en.idusagecategory==idusagecategory)&&
            (string.IsNullOrEmpty(idindustrycategory) || en.idindustrycategory == idindustrycategory) &&
            (string.IsNullOrEmpty(idproductcategory) || en.idproductcategory == idproductcategory) &&
            (string.IsNullOrEmpty(name) || en.name.Contains(name) )
            ).ToList();
            return list;
        }

        public static Product Get(string idproduct)
        {
            TN db = new TN();
            return db.Products.Where(en => en.idproduct == idproduct).FirstOrDefault();
        }

        public static Product Edit(Product product)
        {
            TN db = new TN();
            Product oldProduct = db.Products.Where(en => en.idproduct == product.idproduct).FirstOrDefault();

            oldProduct.idproduct = product.idproduct;
            oldProduct.name = product.name;
            oldProduct.basicinfo = product.basicinfo;
            oldProduct.img = product.img;
            oldProduct.idusagecategory = product.idusagecategory;
            oldProduct.idindustrycategory = product.idindustrycategory;
            oldProduct.idproductcategory = product.idproductcategory;
            oldProduct.description = product.description;
            oldProduct.sortno = product.sortno;
            oldProduct.inuse = product.inuse;
            db.SaveChanges();
            return oldProduct;
        }



        public static Product Add(Product product)
        {
            TN db = new TN();
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        public static int MaxProductImageSort(ModuleType moduleType,string idproduct)
        {
            TN db = new TN();
            return db.Products.Where(en => en.idproduct == idproduct).Max(en => en.sortno) ?? 0;
        }


        public static bool SetDefaultImage(string idproduct)
        {
            bool result = false;
            try
            {
                TN db = new TN();
                Picture firstImage = db.Pictures.Where(en => en.idmodule == idproduct&&en.moduletype==(int)ModuleType.Product).OrderBy(en => en.sortno).First();
                string imagPath = firstImage == null ? "" : firstImage.path;
                Product product = db.Products.Find(idproduct);
                product.img = imagPath;
                db.SaveChanges();

            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
