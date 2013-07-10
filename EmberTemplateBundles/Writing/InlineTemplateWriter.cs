using System.IO;

namespace EmberTemplateBundles.Writing
{
    public class InlineTemplateWriter
    {
        private readonly TemplateNamer _templateNamer;

        public InlineTemplateWriter(TemplateNamer templateNamer)
        {
            _templateNamer = templateNamer;
        }

        public string WriteTemplate(FileInfo file)
        {
            var name = _templateNamer.GetName(file);
            var content = File.ReadAllText(file.FullName);

            return string.Format("<script type='text/x-handlebars' data-template-name='{0}'>\n{1}\n</script>", name, content);
        }
    }
}
