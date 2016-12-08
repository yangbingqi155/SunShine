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
                ProductCategory category = categories.Find(en => model.idcategory == en.idcategory);
                viewModel.categoryName = category != null ? category.categoryname : "";
               
                return viewModel;
            }).ToList();
            return entities;
        }

        public static List<ProductViewModel> SearchViewModels(string name = "", string idcategory = "") {
            List<Product> list = Search(name, idcategory);
            return ConvertToViewModel(list);
        }

        public static List<Product> Search(string name="",string idcategory = "") {
            List<Product> list = new List<Product>();
            TN db = new TN();
            list=db.Products.Where(en=> 
            (string.IsNullOrEmpty(idcategory) ||en.idcategory == idcategory) &&
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
            oldProduct.idcategory = product.idcategory;
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
