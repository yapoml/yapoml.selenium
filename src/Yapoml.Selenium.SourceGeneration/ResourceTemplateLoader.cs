using Scriban;
using Scriban.Parsing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Yapoml.Selenium.SourceGeneration
{
    internal class ResourceTemplateLoader : Scriban.Runtime.ITemplateLoader
    {
        private readonly Assembly _assembly = typeof(ResourceTemplateLoader).Assembly;

        public string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName)
        {
            return $"{_assembly.GetName().Name}.Templates.{templateName}.scriban";
        }

        public string Load(TemplateContext context, SourceSpan callerSpan, string templatePath)
        {
            using (Stream stream = _assembly.GetManifestResourceStream(templatePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public async ValueTask<string> LoadAsync(TemplateContext context, SourceSpan callerSpan, string templatePath)
        {
            using (Stream stream = _assembly.GetManifestResourceStream(templatePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
