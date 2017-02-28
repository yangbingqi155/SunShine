using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SunShine.Web {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           // routes.MapRoute(
           //    name: "product",
           //    url: "Product/List_{idproduct}_{idcategory}_{categoryCode}_{keyword}_{pageIndex}.html",
           //    defaults: new
           //    {
           //        controller = "Product",
           //        action = "List",
           //        idproduct = UrlParameter.Optional,
           //        idcategory = UrlParameter.Optional,
           //        categoryCode = UrlParameter.Optional,
           //        keyword = UrlParameter.Optional,
           //        pageIndex = UrlParameter.Optional
           //    }
           //);

            routes.MapRoute(
                name: "Default_html",
                url: "{controller}/{action}.html",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

           

        }
    }
}
