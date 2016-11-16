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
    public class SiteCategoryViewModel {
        [Display(Name ="类别编号")]
        [StringLength(50)]
        public string idcategory { get; set; }

        [Display(Name = "名称")]
        [Required]
        [StringLength(100)]
        public string categoryname { get; set; }

        [Display(Name = "父级编号")]
        [StringLength(50)]
        public string parentid { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? cretime { get; set; }

        [Display(Name = "启用")]
        public bool inuse { get; set; }


        public void CopyFromBase(SiteCategory category) {
            this.idcategory = category.idcategory;
            this.categoryname = category.categoryname;
            this.inuse = category.inuse;
            this.cretime = category.cretime;
            this.parentid = category.parentid;
        }

        public void CopyToBase(SiteCategory category) {
            category.idcategory = this.idcategory;
            category.categoryname = this.categoryname;
            category.inuse = this.inuse;
            category.cretime = this.cretime;
            category.parentid = this.parentid;
        }
    }
}
