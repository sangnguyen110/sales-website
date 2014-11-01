using FluentValidation.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HoangViet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
           // DependencyConfig.RegisterServices(new Autofac.ContainerBuilder());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
          //  BootstrapEditorTemplatesConfig.RegisterBundles();
          //  Database.SetInitializer<HoangVietDbContext>(new HoangVietDbInitializer());

            
            FluentValidationModelValidatorProvider.Configure();
        }
    }
}
