using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;
using EmberTemplateBundles.Writing;

namespace EmberTemplateBundles
{
    public static class EmberTemplates
    {
        private static HttpContextBase Context
        {
            get
            {
                return new HttpContextWrapper(HttpContext.Current);
            }
        }


        public static IHtmlString Render(string path)
        {
            if (BundleTable.EnableOptimizations)
                return Scripts.Render(path);
            
            var bundle = GetBundle(path);
            var emberTemplateTransform = GetEmberTemplatesTransform(bundle);

            var inlineTemplateWriter = new InlineTemplateWriter(new TemplateNamer(emberTemplateTransform.TemplatesRoot));
            var bundleResponse = bundle.GenerateBundleResponse(new BundleContext(Context, BundleTable.Bundles, path));

            var stringBuilder = new StringBuilder();
            foreach (FileInfo file in bundleResponse.Files)
            {
                stringBuilder.AppendLine(inlineTemplateWriter.WriteTemplate(file));
            }
                
            return new HtmlString(stringBuilder.ToString());
        }

        private static EmberTemplatesTransform GetEmberTemplatesTransform(Bundle bundle)
        {
            var emberTemplateTransform = bundle.Transforms.OfType<EmberTemplatesTransform>().FirstOrDefault();
            if (emberTemplateTransform == null)
                throw new NullReferenceException(string.Format("Referenced ember template bundle does not contain a transform of type {0}", typeof (EmberTemplatesTransform).Name));
            return emberTemplateTransform;
        }

        private static Bundle GetBundle(string path)
        {
            var bundle = BundleTable.Bundles.GetBundleFor(path);
            if (bundle == null)
                throw new ArgumentException("Path does not correspond to any registered bundles", path);
            return bundle;
        }
    }
}
