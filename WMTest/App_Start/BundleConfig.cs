using System.Web;
using System.Web.Optimization;

namespace WMTest
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // The jQuery bundle
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/kendo/2013.2.716/jquery.*"));
            

        }
    }
}