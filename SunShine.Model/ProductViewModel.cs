﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.EF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunShine.Model {
    [NotMapped]
    public class ProductViewModel :Product{
        [Display(Name = "产品编号")]
        [StringLength(50)]
        public new string idproduct { get; set; }

        [Display(Name = "名称")]
        [Required]
        [StringLength(100)]
        public new string name { get; set; }

        [Display(Name = "基本信息")]
        [StringLength(5000)]
        public new string basicinfo { get; set; }

        [Display(Name = "图片")]
        [StringLength(500)]
        public new string img { get; set; }

        [Display(Name = "类别编号")]
        [StringLength(50)]
        public new string idcategory { get; set; }

        [Display(Name = "类别")]
        public string categoryName { get; set; }

        [Display(Name = "用途类别")]
        public string idusagecategory { get; set; }

        [Display(Name = "行业类别")]
        public string idindustrycategory { get; set; }

        [Display(Name = "产品类别")]
        public string idproductcategory { get; set; }

        [Display(Name = "详细描述")]
        public new string description { get; set; }

        [Display(Name = "创建时间")]
        public new DateTime? cretime { get; set; }

        [Display(Name = "排序")]
        public new int? sortno { get; set; }

        [Display(Name = "启用")]
        public new bool inuse { get; set; }


        public void CopyFromBase(Product product) {
            this.idproduct = product.idproduct;
            this.name = product.name;
            this.basicinfo = product.basicinfo;
            this.img = product.img;
            this.idcategory = product.idcategory;
            this.description = product.description;
            this.cretime = product.cretime;
            this.sortno = product.sortno;
            this.inuse = product.inuse;
        }

        public void CopyToBase(Product product) {
            product.idproduct = this.idproduct;
            product.name = this.name;
            product.basicinfo = this.basicinfo;
            product.img = this.img;
            product.idcategory = this.idcategory;
            product.description = this.description;
            product.cretime = this.cretime;
            product.sortno = this.sortno;
            product.inuse = this.inuse;
        }
    }
}
