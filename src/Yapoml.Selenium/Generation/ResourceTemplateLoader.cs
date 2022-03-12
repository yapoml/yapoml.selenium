using Scriban;
using Scriban.Parsing;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Yapoml.Selenium.Generation
{
    internal class ResourceTemplateLoader : Scriban.Runtime.ITemplateLoader
    {
        private Assembly _assembly = typeof(ResourceTemplateLoader).Assembly;

        public string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName)
        {
            return Path.Combine(Environment.CurrentDirectory, templateName);
        }

        public string Load(TemplateContext context, SourceSpan callerSpan, string templatePath)
        {
            // Template path was produced by the `GetPath` method above in case the Template has 
            // not been loaded yet
            using (Stream stream = _assembly.GetManifestResourceStream(_assembly.GetName().Name + ".Generation.Templates." + Path.GetFileName(templatePath) + ".scriban"))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public async ValueTask<string> LoadAsync(TemplateContext context, SourceSpan callerSpan, string templatePath)
        {
            using (Stream stream = _assembly.GetManifestResourceStream(_assembly.GetName().Name + ".Generation.Templates." + Path.GetFileName(templatePath) + ".scriban"))
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
