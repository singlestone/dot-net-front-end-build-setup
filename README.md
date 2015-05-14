## Recommended .NET Front-End Build Setup

This is a sample ASP.NET MVC5 Web Application with build configuration for static resources (pre-processing, bundling, minification, map-file generation, etc.).  Since it favors using tools and extensions from the .NET ecosystem, it is geared towards the more conservative .NET developer.  Listed below are the steps used to set this project up after instantiating the initial project template in Visual Studio 2013.



---


### Initial Cleanup

Disable BrowserLink by adding the following to web.config:

```XML
<appSettings>
    <add key="vs:EnableBrowserLink" value="false" />
</appSettings>
```

Update JavaScript libraries via NuGet to get latest versions (template is probably out-of-date)

Dumping all JavaScript library files into the default Scripts directory makes a mess.  Instead, create a Scripts/lib directory, make subdirectories within it for each JavaScript library, and then use this new directory structure to organize JavaScript library files.

Remove JavaScript library entries from packages.config so they never get accidentally updated by someone via NuGet or unintentionally restored to their old locations via NuGet Package Restore


### Bundling and Minification

Update BundleConfig.cs so that file locations of bundled files are updated to match the new directory structure

```C#
bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/lib/jquery/jquery-{version}.js"));
```

Enable optimizations in BundleConfig.cs so we can validate bundling and minification

```C#
BundleTable.EnableOptimizations = true;
```

Add AspNetBundling via NuGet.  Use the UI or issue the following command from the Visual Studio Package Manager Console:

```
Install-Package AspNetBundling
```

Update BundleConfig.cs to generate source maps for minified JavaScript bundles

```C#
bundles.Add(new ScriptWithSourceMapBundle("~/bundles/jquery").Include(
           "~/Scripts/lib/jquery/jquery-{version}.js"));
```

### Pre-Processing LESS Files

Install dotless via NuGet.  Use the UI or issue the following command from the Visual Studio Package Manager Console:

```
Install-Package dotless
```

Add to web.config to prevent HttpHandler error:  

```XML
<system.webServer>
    <validation validateIntegratedModeConfiguration="false" />
</system.webServer>
```

Create LessTranform.cs class to implement the LESS transform: 

```C#
public class LessTransform : IBundleTransform
{
    public void Process(BundleContext context, BundleResponse response)
    {
        response.Content = Less.Parse(response.Content);
        response.ContentType = "text/css";
    }
}
```

Update BundleConfig.cs to use the new transform for LESS files:

```C#
var styleBundle = new StyleBundle("~/Content/css").Include(
    "~/Content/bootstrap.css",
    "~/Content/site.css",
    "~/Content/colors.less",
    "~/Content/header.less");
styleBundle.Transforms.Add(new LessTransform());
styleBundle.Transforms.Add(new CssMinify());
bundles.Add(styleBundle);
```

### JavaScript Testing

JavaScript Testing should (at a minimum) include Karma as the test runner.  Anything else is lame.  This means that we're going to use Node.js.  You'll need to install Node.js on each development machine that will run JavaScript tests (including the build server).  The following is the current recommendation for Visual Studio setup.

Install Node.js from https://nodejs.org/

Install Karma via npm:

```
npm install karma 
```

