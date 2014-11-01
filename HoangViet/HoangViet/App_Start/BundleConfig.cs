using System.Web.Optimization;

namespace HoangViet
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/themes/bootstrap/js/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/themes/js/bootshop.js",
                      "~/themes/js/jquery.lightbox-0.5.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/themes/bootstrap/css/bootstrap.css",
                      "~/themes/css/base.css",
                      "~/themes/bootstrap/css/bootstrap-responsive.css",
                      "~/themes/css/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrapadmin").Include(
           "~/Areas/Admin/js/bootstrap.js",
           "~/Areas/Admin/js/AdminLTE/app.js"));

            bundles.Add(new StyleBundle("~/Content/cssadmin").Include(
                "~/Areas/Admin/css/bootstrap.css",
                "~/Areas/Admin/css/font-awesome.css",
                "~/Areas/Admin/css/ionicons.css",
                "~/Areas/Admin/css/AdminLTE.css"));

            bundles.Add(new StyleBundle("~/Content/lightboxcss").Include(
                "~/Areas/Admin/css/ekko-lightbox.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/lightboxjs").Include("~/Areas/Admin/js/ekko-lightbox.js"));

            bundles.Add(new ScriptBundle("~/Scripts/themeswitchjs").Include("~/themes/switch/themeswitcher.js"));

            bundles.Add(new StyleBundle("~/Content/themeswitchcss").Include("~/themes/switch/themeswitch.css"));
        }
    }
}
