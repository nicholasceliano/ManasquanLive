using System.Web;
using System.Web.Optimization;

namespace ManasquanLive
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                    "~/Scripts/jquery.flip.min.js",      
                    "~/Scripts/angular.min.js",
                    "~/Scripts/jquery-1.10.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                    "~/Scripts/typings/custom/leftPanel.js",
                    "~/Scripts/typings/custom/rightPanel.js",
                    "~/Scripts/typings/custom/page.js",
                    "~/Scripts/typings/custom/utility.js",
                    "~/Scripts/typings/custom/maps.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Styles/bootstrap.css",
                      "~/Content/Styles/Page.css",
                      "~/Content/Styles/site.css"));
        }
    }
}
