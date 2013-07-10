using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EmberTemplateBundles.Writing
{
    public class TemplateNamer
    {
        private readonly string _templatesRoot;

        public TemplateNamer(string templatesRoot)
        {
            var absoluteRoot = HttpContext.Current.Server.MapPath(templatesRoot);
            _templatesRoot = absoluteRoot;
        }

        public string GetName(FileInfo file)
        {
            var indexOfRoot = file.FullName.IndexOf(_templatesRoot, StringComparison.OrdinalIgnoreCase);
            if (indexOfRoot == 0)
            {
                var indexOfExtension = file.FullName.LastIndexOf('.');
                var nameParts = file.FullName.Substring(_templatesRoot.Length, (indexOfExtension - _templatesRoot.Length)).Split(new[] { '\\' });
                return String.Join("/", GetContributingParts(nameParts));

            }
            throw new ArgumentException(string.Format("Template not within TemplatesRoot ({0})", _templatesRoot));
        }

        private IEnumerable<string> GetContributingParts(string[] nameParts)
        {
            return nameParts.TakeWhile((t, i) => i != nameParts.Length - 1 || t != "default");
        }
    }
}