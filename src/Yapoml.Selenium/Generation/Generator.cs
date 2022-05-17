using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Scriban;
using Scriban.Runtime;
using Yapoml.Framework.Workspace;
using Yapoml.Framework.Workspace.Parsers;

namespace Yapoml.Selenium.Generation
{
    [Generator]
    internal class Generator : ISourceGenerator
    {
        private GeneratorExecutionContext _context;

        private TemplateContext _templateContext;

        private string _rootNamespace;

        public void Initialize(GeneratorInitializationContext context)
        {
            _templateContext = new TemplateContext();
            _templateContext.TemplateLoader = new ResourceTemplateLoader();
            _templateContext.AutoIndent = true;
        }

        public void Execute(GeneratorExecutionContext context)
        {
            _context = context;

            try
            {
                // get root namespace
                context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.RootNamespace", out _rootNamespace);
                context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ProjectDir", out var projectDir);

                var parser = new WorkspaceParser();

                // build yapoml generation context
                var yaContextBuilder = new WorkspaceContextBuilder(projectDir, _rootNamespace, parser);

                foreach (AdditionalText file in context.AdditionalFiles
                    .Where(f => f.Path.EndsWith(".po.yaml", StringComparison.OrdinalIgnoreCase) ||
                        f.Path.EndsWith(".pc.yaml", StringComparison.OrdinalIgnoreCase)))
                {
                    yaContextBuilder.AddFile(file.Path);
                }

                var yaContext = yaContextBuilder.Build();

                // generate files
                GenerateEntryPoint(yaContext);
                GenerateBasePage(yaContext);
                GenerateBaseComponent(yaContext);

                foreach (var space in yaContext.Spaces)
                {
                    GenerateSpace(space);

                    foreach (var component in space.Components)
                    {
                        GenerateComponent(component);
                    }
                }

                foreach (var page in yaContext.Pages)
                {
                    GeneratePage(page);
                }

                foreach (var component in yaContext.Components)
                {
                    GenerateComponent(component);
                }
            }
            catch (Exception exp)
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor(
                    "YA0001",
                    exp.Message,
                    exp.ToString(),
                    "some category",
                    DiagnosticSeverity.Error,
                    true), null));
            }
        }

        private void GenerateEntryPoint(WorkspaceContext workspaceContext)
        {
            var template = Template.Parse(new TemplateReader().Read("_EntryPointTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(workspaceContext));
            var renderedEntryPoint = template.Render(_templateContext);

            _context.AddSource("_EntryPoint.cs", renderedEntryPoint);
        }

        private void GenerateBasePage(WorkspaceContext workspaceContext)
        {
            var template = Template.Parse(new TemplateReader().Read("_BasePageTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(workspaceContext));
            var renderedbasePage = template.Render(_templateContext);

            _context.AddSource("_BasePage.cs", renderedbasePage);
        }

        private void GenerateBaseComponent(WorkspaceContext workspaceContext)
        {
            var template = Template.Parse(new TemplateReader().Read("_BaseComponentTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(workspaceContext));
            var renderedbaseComponent = template.Render(_templateContext);

            _context.AddSource("_BaseComponent.cs", renderedbaseComponent);
        }

        private void GenerateSpace(SpaceContext spaceContext)
        {
            var template = Template.Parse(new TemplateReader().Read("SpaceTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(spaceContext));
            var renderedSpace = template.Render(_templateContext);

            var generatedFileName = $"{spaceContext.Namespace}.{spaceContext.Name}Space.cs";
            _context.AddSource(generatedFileName, renderedSpace);

            foreach (var space in spaceContext.Spaces)
            {
                GenerateSpace(space);
            }

            foreach (var page in spaceContext.Pages)
            {
                GeneratePage(page);
            }

            foreach (var component in spaceContext.Components)
            {
                GenerateComponent(component);
            }
        }

        private void GeneratePage(PageContext pageContext)
        {
            var template = Template.Parse(new TemplateReader().Read("PageTemplate"));

            var scripObject = ScriptObject.From(pageContext);
            scripObject.Import(typeof(Services.GenerationService));
            _templateContext.PushGlobal(scripObject);
            var renderedPage = template.Render(_templateContext);

            var generatedFileName = $"{pageContext.Namespace}.{pageContext.Name}Page.cs";
            _context.AddSource(generatedFileName, renderedPage);
        }

        private void GenerateComponent(ComponentContext componentContext)
        {
            var template = Template.Parse(new TemplateReader().Read("ComponentTemplate"));

            var scripObject = ScriptObject.From(componentContext);
            //scripObject.Import(typeof(Services.ByMethodDetector));
            _templateContext.PushGlobal(scripObject);
            var renderedComponent = template.Render(_templateContext);

            var generatedFileName = $"{componentContext.Namespace}.{componentContext.Name}Component.cs";
            _context.AddSource(generatedFileName, renderedComponent);
        }
    }
}

