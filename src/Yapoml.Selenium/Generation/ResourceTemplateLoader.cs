using Scriban;
using Scriban.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Yapoml.Selenium.Generation
{
    internal class ResourceTemplateLoader : Scriban.Runtime.ITemplateLoader
    {
        private readonly Assembly _assembly = typeof(ResourceTemplateLoader).Assembly;

        private readonly IDictionary<string, string> _templates = new Dictionary<string, string>();

        public string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName)
        {
            return $"{_assembly.GetName().Name}.Generation.Templates.{templateName}.scriban";
        }

        public string Load(TemplateContext context, SourceSpan callerSpan, string templatePath)
        {
            if (!_templates.ContainsKey(templatePath))
            {
                using (Stream stream = _assembly.GetManifestResourceStream(templatePath))
                using (StreamReader reader = new StreamReader(stream))
                {
                    _templates[templatePath] = reader.ReadToEnd();
                }
            }

            return _templates[templatePath];
        }

        public async ValueTask<string> LoadAsync(TemplateContext context, SourceSpan callerSpan, string templatePath)
        {
            if (!_templates.ContainsKey(templatePath))
            {
                using (Stream stream = _assembly.GetManifestResourceStream(templatePath))
                using (StreamReader reader = new StreamReader(stream))
                {
                    _templates[templatePath] = await reader.ReadToEndAsync();
                }
            }

            return _templates[templatePath];
        }
    }
}
