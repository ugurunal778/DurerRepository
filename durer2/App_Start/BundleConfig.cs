using System.Web;
using System.Web.Optimization;

namespace durer2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/js/jquery-3.2.1.min.js",
                        "~/js/script.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/js/jquery-3.2.1.min.js",
                        "~/js/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/adminCss").Include(
                      "~/js/bootstrap/css/bootstrap.min.css",
                      "~/css/styles.css"));

            bundles.Add(new StyleBundle("~/css").Include(
                      "~/css/reset.css", 
                      "~/css/main.css"));
        }
    }
}
