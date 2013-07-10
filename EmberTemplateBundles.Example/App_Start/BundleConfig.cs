using System.Web;
using System.Web.Optimization;

namespace EmberTemplateBundles.Example
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/lib")
                .Include("~/Scripts/Libs/jquery-{version}.js")
                .Include("~/Scripts/Libs/handlebars-1.0.0-rc.4.js")
                .Include("~/Scripts/Libs/ember-1.0.0-rc.6.js")
                );
            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/Scripts/App/EmberApp.js")
                );

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            var emberTemplatesTransform = new EmberTemplatesTransform("~/Scripts/Libs/handlebars-1.0.0-rc.4.js", "~/Scripts/Libs/ember-1.0.0-rc.6.js");

            bundles.Add(new Bundle("~/bundles/templates", emberTemplatesTransform)
                    .IncludeDirectory("~/Scripts/app/templates", "*.hbs", true)
                );

        }
    }
}