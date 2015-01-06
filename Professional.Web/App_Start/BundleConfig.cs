using System.Web;
using System.Web.Optimization;

namespace Professional.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScripts(bundles);

            RegisterStyles(bundles);

            //// Set EnableOptimizations to false for debugging. For more information,
            //// visit http://go.microsoft.com/fwlink/?LinkId=301862

            BundleTable.EnableOptimizations = true;
        }

        public static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-1.10.2.min.js",
                "~/Scripts/jquery-ui-1.11.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js"));
                
            bundles.Add(new ScriptBundle("~/bundles/unobtrusive-jquery").Include(
                    "~/Scripts/jquery.unobtrusive-ajax.min.js",
                    "~/Scripts/jquery.validate.unobtrusive.min.js"));

            //bundles.Add(new ScriptBundle("~/Js/kendo").Include(
            //        "~/Scripts/kendo/kendo.all.min.js",
            //        "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/Js/custom").Include(
                    "~/Scripts/custom.js"));

            bundles.Add(new ScriptBundle("~/SingleR/hubs").Include(
                    "~/Scripts/jquery.signalR-2.1.2.min.js",
                    "~/signalr/hubs",
                    "~/Scripts/chat.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/transition.js",
                      "~/Scripts/collapse.js",
                      "~/Scripts/respond.js"));
        }

        public static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/users-css").Include(
                      "~/Content/chat-styles.css"));

            bundles.Add(new StyleBundle("~/Content/jquery-css").Include(
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/themes/base/theme.css",
                      "~/Content/themes/base/datepicker.css"));

            //bundles.Add(new StyleBundle("~/Css/kendo").Include(
            //            "~/Content/kendo/kendo.common.*",
            //            "~/Content/kendo/kendo.silver.*",
            //            "~/Content/kendo/images/",
            //            "~/Content/kendo/custom-kendo.css"));
        }
    }
}
