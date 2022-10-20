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

        public string ProducePage(PageContext pageContext)
        {
            PushIntoTemplateContext(pageContext);

            return _pageTemplate.Render(_ctx);
        }

        public string ProduceComponent(ComponentContext componentContext)
        {
            PushIntoTemplateContext(componentContext);

            return _componentTemplate.Render(_ctx);
        }

        public string ProduceSpace(SpaceContext spaceContext)
        {
            PushIntoTemplateContext(spaceContext);

            return _spaceTemplate.Render(_ctx);
        }

        public string ProduceEntryPoint(WorkspaceContext workspaceContext)
        {
            PushIntoTemplateContext(workspaceContext);

            return _entryPointTemplate.Render(_ctx);
        }

        public string ProduceBasePage(WorkspaceContext workspaceContext)
        {
            PushIntoTemplateContext(workspaceContext);

            return _basePageTemplate.Render(_ctx);
        }

        public string ProduceBaseComponent(WorkspaceContext workspaceContext)
        {
            PushIntoTemplateContext(workspaceContext);

            return _baseComponentTemplate.Render(_ctx);
        }

        private void PushIntoTemplateContext(object obj)
        {
            var scriptObject = ScriptObject.From(obj);
            scriptObject.Import(typeof(Services.GenerationService));

            _ctx.PushGlobal(scriptObject);
        }
    }
}
