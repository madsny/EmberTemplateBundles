using System;
using System.IO;
using System.Web;
using Noesis.Javascript;

namespace EmberTemplateBundles.PreCompile
{
    public class EmberHandlebarsCompiler : IDisposable
    {
        private readonly JavascriptContext _jsContext;

        public EmberHandlebarsCompiler(string handlebarsPath, string emberPath)
        {
            var handlebars = File.ReadAllText(HttpContext.Current.Server.MapPath(handlebarsPath));
            var ember = File.ReadAllText(HttpContext.Current.Server.MapPath(emberPath));
            
            _jsContext = new JavascriptContext();
            _jsContext.Run(WindowAndJQueryStub);
            _jsContext.Run(handlebars);
            _jsContext.Run(ember);
        }

        public string PreCompile(FileInfo input)
        {
            var templateFile = File.ReadAllText(input.FullName);

            _jsContext.SetParameter("myInput", templateFile);
            var noe = _jsContext.Run("Ember.Handlebars.precompile(myInput).toString();");
            return noe.ToString();
        }

        public void Dispose()
        {
            if (_jsContext != null)
                _jsContext.Dispose();
        }


        private const string WindowAndJQueryStub = @"        
            var window = this,
            document = {
              createElement: function(type) {
                return {
                  firstChild: {},
                  childNodes: [{},{},{}]  
                };
              },
              getElementById: function(id) { 
                return [];
              },
              getElementsByTagName: function(tagName) {
                return [];
              }
            },
            location = {
              protocol: 'file:', 
              hostname: 'localhost',
              href: 'http://localhost:80',
              port: '80'
            },
            console = {
              log: function() {},
              info: function() {},
              warn: function() {},
              error: function() {}
            }
        
        // make a dummy jquery object just to make ember happy
        var jQuery = function() { return jQuery; };
        jQuery.ready = function() { return jQuery; };
        jQuery.inArray = function() { return jQuery; };
        jQuery.event = {
                            fixHooks : []
                        };
        jQuery.jquery = '1.7.1';
        var $ = jQuery;";


        

    }
}
