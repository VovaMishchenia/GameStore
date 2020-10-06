using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace GameStoreClient.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js"));

            

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
           

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
           "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
           "~/Content/bootstrap.css",
           "~/Content/style.css"));

            bundles.Add(new ScriptBundle("~/scripts/custom").Include(
                "~/Scripts/custom.js"));
            bundles.Add(new StyleBundle("~/Content/custom").Include(
                "~/Content/custom/custom.css"));
            bundles.Add(new StyleBundle("~/Content/card").Include(
               "~/Content/card/cardstyle.css"));
        }
    }
}