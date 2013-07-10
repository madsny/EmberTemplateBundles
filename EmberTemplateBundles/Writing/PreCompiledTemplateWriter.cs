using System.IO;
using EmberTemplateBundles.PreCompile;

namespace EmberTemplateBundles.Writing
{
    public class PreCompiledTemplateWriter
    {
        private readonly TemplateNamer _templateNamer;
        private readonly EmberHandlebarsCompiler _templateCompiler;

        public PreCompiledTemplateWriter(TemplateNamer templateNamer = null, EmberHandlebarsCompiler templateCompiler = null)
        {
            _templateNamer = templateNamer;
            _templateCompiler = templateCompiler;
        }

        public string WriteTemplate(FileInfo fileInfo)
        {
            var name = _templateNamer.GetName(fileInfo);
            var templateFunction = _templateCompiler.PreCompile(fileInfo);
            return string.Format("Ember.TEMPLATES['{0}'] = Ember.Handlebars.template({1});", name, templateFunction);
        }
    }
}