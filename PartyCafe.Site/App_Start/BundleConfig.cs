using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Optimization;

namespace PartyCafe.Site
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/mainJS").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      "~/Content/css/style.css"));

            /* GAMMA GALLERY */
            bundles.Add(new ScriptBundle("~/bundles/gammagalleryJS").Include(
                    "~/Scripts/GammaGallery/modernizr.custom.70736.js",
                    "~/Scripts/GammaGallery/jquery.masonry.min.js",
                    "~/Scripts/GammaGallery/jquery.history.js",
                    "~/Scripts/GammaGallery/js-url.min.js",
                    "~/Scripts/GammaGallery/jquerypp.custom.js",
                    "~/Scripts/GammaGallery/gamma.js"
            ));
            bundles.Add(new StyleBundle("~/bundles/gammagalleryCSS").Include(
                    "~/Content/css/GammaGallery/style.css",
                    "~/Content/css/GammaGallery/noJS.css",
                    "~/Content/css/GammaGallery/demo.css"
            ));
        }
    }
}
