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
    public class WebSiteInfoService
    {

        public static List<WebSiteInfo> GetALL()
        {
            TN db = new TN();
            return db.WebSiteInfoes.ToList();
        }

        public static WebSiteInfo Get(string idsite)
        {
            TN db = new TN();
            return db.WebSiteInfoes.Where(en => en.idsite == idsite).FirstOrDefault();
        }

        public static WebSiteInfo Edit(WebSiteInfo websiteInfo)
        {
            TN db = new TN();
            WebSiteInfo oldWebsiteInfo = db.WebSiteInfoes.Where(en => en.idsite == websiteInfo.idsite).FirstOrDefault();

            oldWebsiteInfo.idsite = websiteInfo.idsite;
            oldWebsiteInfo.sitename = websiteInfo.sitename;
            oldWebsiteInfo.copyright = websiteInfo.copyright;
            oldWebsiteInfo.address = websiteInfo.address;
            oldWebsiteInfo.phone = websiteInfo.phone;
            oldWebsiteInfo.qq = websiteInfo.qq;

            db.SaveChanges();
            return oldWebsiteInfo;
        }

        public static WebSiteInfo Add(WebSiteInfo websiteInfo)
        {
            TN db = new TN();
            db.WebSiteInfoes.Add(websiteInfo);
            db.SaveChanges();
            return websiteInfo;
        }
    }
}
