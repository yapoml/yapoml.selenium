using System;
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

                foreach (AdditionalText file in context.AdditionalFiles)
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
                    GenerateSpaces(space);
                }

                foreach (var page in yaContext.Pages)
                {
                    GeneratePages(page);
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

        private void GenerateSpaces(SpaceContext spaceContext)
        {
            var template = Template.Parse(new TemplateReader().Read("SpaceTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(spaceContext));
            var renderedSpace = template.Render(_templateContext);

            var generatedFileName = $"{spaceContext.Namespace}.{spaceContext.Name}Space.cs";
            _context.AddSource(generatedFileName, renderedSpace);

            foreach (var space in spaceContext.Spaces)
            {
                GenerateSpaces(space);
            }

            foreach (var page in spaceContext.Pages)
            {
                GeneratePages(page);
            }

            foreach (var component in spaceContext.Components)
            {
                //GenerateComponent(component);
            }
        }

        private void GeneratePages(PageContext pageContext)
        {
            var template = Template.Parse(new TemplateReader().Read("PageTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(pageContext));
            var renderedPage = template.Render(_templateContext);

            var generatedFileName = $"{pageContext.Namespace}.{pageContext.Name}Page.cs";
            _context.AddSource(generatedFileName, renderedPage);

            foreach (var component in pageContext.Components)
            {
                //GenerateComponent(component);
            }
        }

        //private void GenerateComponent(ComponentGenerationContext componentGenerationContext)
        //{
        //    Template engine = Template.Parse(_templateReader.Read("ComponentTemplate"));

        //    var renderedComponent = engine.Render(Hash.FromAnonymousObject(componentGenerationContext));

        //    var generatedFileName = $"{componentGenerationContext.Namespace.Substring(_rootNamespace.Length + 1).Replace('.', '_')}_{componentGenerationContext.Name}Component.g.cs";
        //    _context.AddSource(generatedFileName, renderedComponent);

        //    foreach (var component in componentGenerationContext.ComponentGenerationContextes)
        //    {
        //        GenerateComponent(component);
        //    }
        //}
    }
}

