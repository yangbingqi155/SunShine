using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SunShine.EF;

namespace SunShine.Model
{
    public class WebsiteInfoViewModel:WebSiteInfo
    {

        [Display(Name ="编号")]
        [StringLength(50)]
        public new string idsite { get; set; }

        [Display(Name = "网站名称")]
        [StringLength(500)]
        public new string sitename { get; set; }

        [Display(Name = "Copyright")]
        [StringLength(500)]
        public new string copyright { get; set; }

        [Display(Name = "地址")]
        [StringLength(500)]
        public new string address { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(500)]
        public new string phone { get; set; }

        [Display(Name = "QQ")]
        [StringLength(500)]
        public new string qq { get; set; }


        public void CopyFromBase(WebSiteInfo websiteInfo)
        {
            this.idsite = websiteInfo.idsite;
            this.sitename = websiteInfo.sitename;
            this.copyright = websiteInfo.copyright;
            this.address = websiteInfo.address;
            this.phone = websiteInfo.phone;
            this.qq = websiteInfo.qq;
        }

        public void CopyToBase(WebSiteInfo websiteInfo)
        {
            websiteInfo.idsite = this.idsite;
            websiteInfo.sitename = this.sitename;
            websiteInfo.copyright = this.copyright;
            websiteInfo.address = this.address;
            websiteInfo.phone = this.phone;
            websiteInfo.qq = this.qq;
        }
    }
}
