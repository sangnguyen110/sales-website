using System.Web.Mvc;
using System.Web.Routing;

namespace HoangViet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
   "productnocategory",
   "{id}_{slug}",
   new { controller = "product", action = "details" },
   namespaces: new string[] { "HoangViet.Controllers" }
   );
            routes.MapRoute(
       "checkout",
       "checkout",
       new { controller = "checkout", action = "index" },
       namespaces: new string[] { "HoangViet.Controllers" }
       );
            routes.MapRoute(
"categorywithpage",
"{category}/{page}",
new { controller = "category", action = "details" }, new { page = @"\d+" },
namespaces: new string[] { "HoangViet.Controllers" }
);
            routes.MapRoute(
"category",
"{category}",
new { controller = "category", action = "details" },
namespaces: new string[] { "HoangViet.Controllers" }
);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
          "search",
          "category/search/{searchterm}/{searchcategory}/{page}",
          new { controller = "category", action = "search", searchcategory = UrlParameter.Optional, page = UrlParameter.Optional },
          namespaces: new string[] { "HoangViet.Controllers" }
          );

            routes.MapRoute(
            "product",
            "{category}/{id}_{slug}",
            new { controller = "product", action = "details" },
            namespaces: new string[] { "HoangViet.Controllers" }
            );

            routes.MapRoute(
           "shoppingcart",
           "shoppingcart/{action}",
           new { controller = "shoppingcart", action = "index" },
           namespaces: new string[] { "HoangViet.Controllers" }
           );

            routes.MapRoute(
name: "Default",
url: "{controller}/{action}/{id}",
defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
namespaces: new[]{ "HoangViet.Controllers" }
);



        }
    }
}