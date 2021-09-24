using System.Web;
using System.Web.Optimization;

namespace OnlineNews
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/content/backend/plugins/jquery/jquery.min.js",
                "~/content/backend/plugins/bootstrap/js/bootstrap.bundle.min.js",
                "~/content/backend/plugins/datatables/jquery.dataTables.min.js",
                "~/content/backend/plugins/datatables/dataTables.bootstrap4.min.js",
                "~/content/backend/plugins/slimScroll/jquery.slimscroll.min.js",
                "~/content/backend/plugins/fastclick/fastclick.js",
                "~/content/backend/plugins/iCheck/icheck.min.js",
                "~/content/backend/plugins/select2/select2.full.min.js",
                "~/content/backend/dist/js/adminlte.min.js",
                "~/content/backend/site.js"));


            bundles.Add(new StyleBundle("~/css").Include(
                        "~/Content/backend/plugins/iCheck/square/green.css"
                        , "~/Content/backend/plugins/datatables/dataTables.bootstrap4.min.css"
                        , "~/Content/backend/plugins/select2/select2.min.css"
                        , "~/Content/backend/dist/css/adminlte.min.css"
                        , "~/Content/backend/site.css"
                      )
           .Include("~/Content/backend/plugins/font-awesome/fontawesome-5.5.0/css/all.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            ////.............Frontend................////
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Content/js/jquery.js",
                "~/Content/js/popper.min.js",
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/jquery.magnific-popup.min.js",
                "~/Content/js/owl.carousel.min.js",
                "~/Content/js/moment.min.js",
                "~/Content/js/jquery.sticky-kit.min.js",
                "~/Content/js/jquery.easy-ticker.min.js",
                "~/Content/js/jquery.subscribe-better.min.js",
                "~/Content/js/theia-sticky-sidebar.min.js",
                "~/Content/js/main.js",
                "~/Content/js/switcher.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/magnific-popup.css",
                "~/Content/css/owl.carousel.css",
                "~/Content/css/subscribe-better.css",
                "~/Content/css/main.css",
                "~/Content/css/responsive.css"));
            ////.............Frontend................////

            BundleTable.EnableOptimizations = true;

        }
    }
}
