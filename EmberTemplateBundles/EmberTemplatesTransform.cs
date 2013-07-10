using System.Text;
using System.Web.Optimization;
using EmberTemplateBundles.PreCompile;
using EmberTemplateBundles.Writing;

namespace EmberTemplateBundles
{
    public class EmberTemplatesTransform : IBundleTransform
    {
        private readonly string _handlebarsPath;
        private readonly string _emberPath;
        private readonly IBundleTransform _nextTransform;

        public EmberTemplatesTransform(string handlebarsPath, string emberPath, IBundleTransform nextTransform = null)
        {
            _handlebarsPath = handlebarsPath;
            _emberPath = emberPath;
            TemplatesRoot = "~/Scripts/App/Templates/";
            _nextTransform = nextTransform ?? new JsMinify();
        }

        private string _templatesRoot;
        public string TemplatesRoot
        {
            get { return _templatesRoot + (_templatesRoot.EndsWith("/") ? string.Empty : "/"); }
            set { _templatesRoot = value; }
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            using (var templateCompiler = new EmberHandlebarsCompiler(_handlebarsPath, _emberPath))
            {
                var templateWriter = new PreCompiledTemplateWriter(new TemplateNamer(TemplatesRoot), templateCompiler);
                var sb = new StringBuilder();
                foreach (var fileInfo in response.Files)
                {
                    sb.AppendLine(templateWriter.WriteTemplate(fileInfo));
                }
                response.Content = sb.ToString();
            }
            _nextTransform.Process(context, response);
        }
    }
}
