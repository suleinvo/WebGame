using System.Web;
using System.Web.Optimization;

namespace WebGame.App_Start
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/kinetic").Include(
                "~/Scripts/kinetic-v5.1.0.min.js"));
        }
    }
}