using System.Web.Optimization;
using AspNetBundling;

namespace FrontEndBuild.DotNetSolution
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jquery").Include(
                "~/Scripts/lib/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jqueryval").Include(
                "~/Scripts/lib/jquery.validate/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/modernizr").Include(
                "~/Scripts/lib/modernizr/modernizr-*"));

            bundles.Add(new ScriptWithSourceMapBundle("~/bundles/bootstrap").Include(
                "~/Scripts/lib/bootstrap/bootstrap.js",
                "~/Scripts/lib/respond/respond.js"));

            var styleBundle = new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/colors.less",
                "~/Content/header.less");
            styleBundle.Transforms.Add(new LessTransform());
            styleBundle.Transforms.Add(new CssMinify());

            bundles.Add(styleBundle);
        }
    }
}
