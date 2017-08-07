using System.Web;
using System.Web.Optimization;

namespace MyArt
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      ));
        }
    }
}
