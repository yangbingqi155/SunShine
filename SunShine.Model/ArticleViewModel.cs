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
    public class ArticleViewModel:Article {
        [Display(Name = "文章编号")]
        [StringLength(50)]
        public new string idarticle { get; set; }

        [Display(Name = "标题")]
        [Required]
        [StringLength(100)]
        public new string title { get; set; }

        [Display(Name = "标题图")]
        [StringLength(500)]
        public new string img { get; set; }

        [Display(Name = "内容")]
        public new string content { get; set; }

        [Display(Name = "网站类别")]
        [Required]
        [StringLength(50)]
        public new string idcategory { get; set; }

        [Display(Name = "关注量")]
        public new int? follow { get; set; }

        [Display(Name = "发布时间")]
        public new DateTime? cretime { get; set; }
        
        public void CopyFromBase(Article article) {
            this.idarticle = article.idarticle;
            this.title = article.title;
            this.img = article.img;
            this.content = article.content;
            this.idcategory = article.idcategory;
            this.follow = article.follow;
            this.cretime = article.cretime;
        }

        public void CopyToBase(Article article) {
            article.idarticle = this.idarticle;
            article.title = this.title;
            article.img = this.img;
            article.content = this.content;
            article.idcategory = this.idcategory;
            article.follow = this.follow;
            article.cretime = this.cretime;
        }
    }
}