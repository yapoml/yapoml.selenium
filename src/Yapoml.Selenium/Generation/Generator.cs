using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Scriban;
using Scriban.Runtime;
using Yapoml.Generation;
using Yapoml.Generation.Parsers;

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

                var parser = new Parser();

                // build yapoml generation context
                var yaContext = new GlobalGenerationContext(projectDir, _rootNamespace, parser);

                foreach (AdditionalText file in context.AdditionalFiles)
                {
                    yaContext.AddFile(file.Path);
                }

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

        private void GenerateEntryPoint(GlobalGenerationContext globalGenerationContext)
        {
            var template = Template.Parse(new TemplateReader().Read("_EntryPointTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(globalGenerationContext));
            var renderedEntryPoint = template.Render(_templateContext);

            _context.AddSource("_EntryPoint.cs", renderedEntryPoint);
        }

        private void GenerateBasePage(GlobalGenerationContext globalGenerationContext)
        {
            var template = Template.Parse(new TemplateReader().Read("_BasePageTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(globalGenerationContext));
            var renderedbasePage = template.Render(_templateContext);

            _context.AddSource("_BasePage.cs", renderedbasePage);
        }

        private void GenerateBaseComponent(GlobalGenerationContext globalGenerationContext)
        {
            var template = Template.Parse(new TemplateReader().Read("_BaseComponentTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(globalGenerationContext));
            var renderedbaseComponent = template.Render(_templateContext);

            _context.AddSource("_BaseComponent.cs", renderedbaseComponent);
        }

        private void GenerateSpaces(SpaceGenerationContext spaceGenerationContext)
        {
            var template = Template.Parse(new TemplateReader().Read("SpaceTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(spaceGenerationContext));
            var renderedSpace = template.Render(_templateContext);

            var generatedFileName = $"{spaceGenerationContext.Namespace}.{spaceGenerationContext.Name}Space.cs";
            _context.AddSource(generatedFileName, renderedSpace);

            foreach (var space in spaceGenerationContext.Spaces)
            {
                GenerateSpaces(space);
            }

            foreach (var page in spaceGenerationContext.Pages)
            {
                GeneratePages(page);
            }

            foreach (var component in spaceGenerationContext.Components)
            {
                //GenerateComponent(component);
            }
        }

        private void GeneratePages(PageGenerationContext pageGenerationContext)
        {
            var template = Template.Parse(new TemplateReader().Read("PageTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(pageGenerationContext));
            var renderedPage = template.Render(_templateContext);

            var generatedFileName = $"{pageGenerationContext.Namespace}.{pageGenerationContext.Name}Page.cs";
            _context.AddSource(generatedFileName, renderedPage);

            foreach (var component in pageGenerationContext.Components)
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

