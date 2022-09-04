using Scriban;
using Scriban.Runtime;

namespace Yapoml.Selenium.Generation
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

        public string ProducePage(Framework.Workspace.PageContext pageContext)
        {
            PushIntoTemplateContext(pageContext);

            return _pageTemplate.Render(_ctx);
        }

        public string ProduceComponent(Framework.Workspace.ComponentContext componentContext)
        {
            PushIntoTemplateContext(componentContext);

            return _componentTemplate.Render(_ctx);
        }

        public string ProduceSpace(Framework.Workspace.SpaceContext spaceContext)
        {
            PushIntoTemplateContext(spaceContext);

            return _spaceTemplate.Render(_ctx);
        }

        public string ProduceEntryPoint(Framework.Workspace.WorkspaceContext workspaceContext)
        {
            PushIntoTemplateContext(workspaceContext);

            return _entryPointTemplate.Render(_ctx);
        }

        public string ProduceBasePage(Framework.Workspace.WorkspaceContext workspaceContext)
        {
            PushIntoTemplateContext(workspaceContext);

            return _basePageTemplate.Render(_ctx);
        }

        public string ProduceBaseComponent(Framework.Workspace.WorkspaceContext workspaceContext)
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
