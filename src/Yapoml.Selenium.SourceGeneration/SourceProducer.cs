using Scriban;
using Scriban.Runtime;
using Yapoml.Framework.Workspace;

namespace Yapoml.Selenium.SourceGeneration
{
    internal class SourceProducer
    {
        private readonly TemplateContext _ctx;

        private readonly Template _pageTemplate;
        private readonly Template _componentTemplate;
        private readonly Template _spaceTemplate;
        private readonly Template _entryPointTemplate;
        private readonly Template _basePageTemplate;
        private readonly Template _baseComponentTemplate;

        public SourceProducer()
        {
            var templateLoader = new ResourceTemplateLoader();

            _ctx = new TemplateContext
            {
                TemplateLoader = templateLoader,
                AutoIndent = true
            };

            var templateReader = new TemplateReader();

            _entryPointTemplate = Template.Parse(templateReader.Read("_EntryPointTemplate"));
            _basePageTemplate = Template.Parse(templateReader.Read("_BasePageTemplate"));
            _baseComponentTemplate = Template.Parse(templateReader.Read("_BaseComponentTemplate"));

            _pageTemplate = Template.Parse(templateReader.Read("PageTemplate"));
            _componentTemplate = Template.Parse(templateReader.Read("ComponentTemplate"));
            _spaceTemplate = Template.Parse(templateReader.Read("SpaceTemplate"));
        }

        private string Render(Template template, object model)
        {
            var scriptObject = ScriptObject.From(model);
            scriptObject.Import(typeof(Services.GenerationService));

            _ctx.PushGlobal(scriptObject);

            var res = template.Render(_ctx);

            _ctx.PopGlobal();

            return res;
        }

        public string ProducePage(PageContext pageContext)
        {
            return Render(_pageTemplate, pageContext);
        }

        public string ProduceComponent(ComponentContext componentContext)
        {
            return Render(_componentTemplate, componentContext);
        }

        public string ProduceSpace(SpaceContext spaceContext)
        {
            return Render(_spaceTemplate, spaceContext);
        }

        public string ProduceEntryPoint(WorkspaceContext workspaceContext)
        {
            return Render(_entryPointTemplate, workspaceContext);
        }

        public string ProduceBasePage(WorkspaceContext workspaceContext)
        {
            return Render(_basePageTemplate, workspaceContext);
        }

        public string ProduceBaseComponent(WorkspaceContext workspaceContext)
        {
            return Render(_baseComponentTemplate, workspaceContext);
        }
    }
}
