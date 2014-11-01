using System.Web.Mvc;

namespace HoangViet.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(null, "connector", new { controller = "Files", action = "Index", area = "admin" });
          //  context.MapRoute(null, "admin/elfinder/thumbnails/{tmb}", new { controller = "Files", action = "Thumbs", tmb = UrlParameter.Optional });
            context.MapRoute(null, "thumbnails/{tmb}", new { area="admin", controller = "Files", action = "Thumbs", tmb = UrlParameter.Optional });
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}