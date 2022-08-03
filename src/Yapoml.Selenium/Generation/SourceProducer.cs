﻿using Scriban;
using Scriban.Runtime;

namespace Yapoml.Selenium.Generation
{
    internal class SourceProducer
    {
        private TemplateReader _templateReader;

        private Template _pageTemplate;
        private Template _componentTemplate;
        private Template _spaceTemplate;
        private Template _entryPointTemplate;
        private Template _basePageTemplate;
        private Template _baseComponentTemplate;

        public SourceProducer()
        {
            _templateReader = new TemplateReader();

            _entryPointTemplate = Template.Parse(_templateReader.Read("_EntryPointTemplate"));
            _basePageTemplate = Template.Parse(_templateReader.Read("_BasePageTemplate"));
            _baseComponentTemplate = Template.Parse(_templateReader.Read("_BaseComponentTemplate"));

            _pageTemplate = Template.Parse(_templateReader.Read("PageTemplate"));
            _componentTemplate = Template.Parse(_templateReader.Read("ComponentTemplate"));
            _spaceTemplate = Template.Parse(_templateReader.Read("SpaceTemplate"));
        }

        public string ProducePage(Framework.Workspace.PageContext pageContext)
        {
            var ctx = CreateTemplateContext(pageContext);
            return _pageTemplate.Render(ctx);
        }

        public string ProduceComponent(Framework.Workspace.ComponentContext componentContext)
        {
            var ctx = CreateTemplateContext(componentContext);
            return _componentTemplate.Render(ctx);
        }

        public string ProduceSpace(Framework.Workspace.SpaceContext spaceContext)
        {
            var ctx = CreateTemplateContext(spaceContext);

            return _spaceTemplate.Render(ctx);
        }

        public string ProduceEntryPoint(Framework.Workspace.WorkspaceContext workspaceContext)
        {
            var ctx = CreateTemplateContext(workspaceContext);

            return _entryPointTemplate.Render(ctx);
        }

        public string ProduceBasePage(Framework.Workspace.WorkspaceContext workspaceContext)
        {
            var ctx = CreateTemplateContext(workspaceContext);

            return _basePageTemplate.Render(ctx);
        }

        public string ProduceBaseComponent(Framework.Workspace.WorkspaceContext workspaceContext)
        {
            var ctx = CreateTemplateContext(workspaceContext);

            return _baseComponentTemplate.Render(ctx);
        }

        private TemplateContext CreateTemplateContext(object obj)
        {
            var scriptObject = ScriptObject.From(obj);
            scriptObject.Import(typeof(Services.GenerationService));

            var templateContext = new TemplateContext();
            templateContext.TemplateLoader = new ResourceTemplateLoader();
            templateContext.AutoIndent = true;

            templateContext.PushGlobal(scriptObject);

            return templateContext;
        }
    }
}