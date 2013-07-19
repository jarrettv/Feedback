using System.Web;
using System.Web.Optimization;

namespace JVance.Feedback.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/js")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery-timeago.js")

                .Include("~/Scripts/bootstrap-transition.js")
                .Include("~/Scripts/bootstrap-tooltip.js")
                .Include("~/Scripts/bootstrap-*")
                );

            bundles.Add(new LessBundle("~/css")
                .Include("~/Styles/bootstrap.less")
                );

        }
    }
}
