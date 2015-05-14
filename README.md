+ Create MVC5 Web Application
+ Disable BrowserLink
+ Update JavaScript libraries via NuGet
+ Create Scripts/lib directory, make subdirectories for each JavaScript library and then use this new directory structure to organize JavaScript library files.
+ Remove JavaScript library entries from packages.config so they never get restored to their old locations
+ Update BundleConfig.cs
+ Enable optimizations in BundleConfig.cs so we can validate bundling and minification
+ Add AspNetBundling via NuGet
+ Update BundleConfig.cs to generate source maps for minified JavaScript bundles
+ Install dotless via NuGet
+ Add to web.config:  

    <system.webServer>
        <validation validateIntegratedModeConfiguration="false" />
    </system.webServer>


+ Create LessTranform.cs and update BundleConfig.cs to use it for LESS files:

    public class LessTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            response.Content = Less.Parse(response.Content);
            response.ContentType = "text/css";
        }
    }

    In BundleConfig.cs:

    var styleBundle = new StyleBundle("~/Content/css").Include(
        "~/Content/bootstrap.css",
        "~/Content/site.css",
        "~/Content/colors.less",
        "~/Content/header.less");
    styleBundle.Transforms.Add(new LessTransform());
    styleBundle.Transforms.Add(new CssMinify());
    bundles.Add(styleBundle);


+ Start up the application to make sure there are no errors related to static resource loading
