using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.EF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunShine.Model {
    [NotMapped]
    public class ProductCategoryViewModel :ProductCategory{

        [Display(Name = "类别编号")]
        [StringLength(50)]
        public new string idcategory { get; set; }

        [Display(Name = "名称")]
        [Required]
        [StringLength(50)]
        public new string categoryname { get; set; }

        [Display(Name = "排序")]
        public new int? sortno { get; set; }

        [Display(Name = "启用")]
        public new bool inuse { get; set; }

        [Display(Name = "分组方式")]
        public new int groupmethod { get; set; }

        [Display(Name = "创建时间")]
        public new DateTime? cretime { get; set; }


        public void CopyFromBase(ProductCategory category) {
            this.idcategory = category.idcategory;
            this.categoryname = category.categoryname;
            this.sortno = category.sortno;
            this.inuse = category.inuse;
            this.groupmethod = category.groupmethod;
            this.cretime = category.cretime;        }

        public void CopyToBase(ProductCategory category) {
            category.idcategory = this.idcategory;
            category.categoryname = this.categoryname;
            category.sortno = this.sortno;
            category.inuse = this.inuse;
            category.groupmethod = this.groupmethod;
            category.cretime = this.cretime;
        }
    }
}
