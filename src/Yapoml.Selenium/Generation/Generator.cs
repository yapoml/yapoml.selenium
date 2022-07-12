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
    internal class Generator : IIncrementalGenerator
    {
        private TemplateContext _templateContext;

        private IWorkspaceParser _parser = new WorkspaceParser();
        private WorkspaceContextBuilder _yaContextBuilder;

        private string _rootNamespace;
        private string _projectDir;

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            _templateContext = new TemplateContext();
            _templateContext.TemplateLoader = new ResourceTemplateLoader();
            _templateContext.AutoIndent = true;

            var globalOptions = context.AnalyzerConfigOptionsProvider.Select((o, c) => o.GlobalOptions);
            context.RegisterSourceOutput(globalOptions, (c, s) =>
            {
                s.TryGetValue("build_property.RootNamespace", out _rootNamespace);
                s.TryGetValue("build_property.ProjectDir", out _projectDir);
            });

            IncrementalValuesProvider<AdditionalText> textFiles = context.AdditionalTextsProvider.Where(file => file.Path.EndsWith(".po.yaml", StringComparison.OrdinalIgnoreCase) || file.Path.EndsWith(".pc.yaml", StringComparison.OrdinalIgnoreCase));

            var namesAndContents = textFiles.Select((text, cancellationToken) => (path: text.Path, content: text.GetText(cancellationToken).ToString())).Collect();

            context.RegisterSourceOutput(namesAndContents, (spc, files) =>
            {
                try
                {
                    // build yapoml generation context
                    _yaContextBuilder = new WorkspaceContextBuilder(_projectDir, _rootNamespace, _parser);

                    foreach (var file in files)
                    {
                        _yaContextBuilder.AddFile(file.path, file.content);
                    }

                    var yaContext = _yaContextBuilder.Build();

                    // generate files
                    if (yaContext.Spaces.Any() || yaContext.Pages.Any() || yaContext.Components.Any())
                    {
                        GenerateEntryPoint(spc, yaContext);
                        GenerateBasePage(spc, yaContext);
                        GenerateBaseComponent(spc, yaContext);

                        foreach (var space in yaContext.Spaces)
                        {
                            GenerateSpace(spc, space);

                            foreach (var component in space.Components)
                            {
                                GenerateComponent(spc, component);
                            }
                        }

                        foreach (var page in yaContext.Pages)
                        {
                            GeneratePage(spc, page);
                        }

                        foreach (var component in yaContext.Components)
                        {
                            GenerateComponent(spc, component);
                        }
                    }
                }
                catch (Exception ex)
                {
                    spc.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor(
                    "YA0001",
                    ex.Message,
                    ex.ToString(),
                    "some category",
                    DiagnosticSeverity.Error,
                    true), null));
                }
            });

        }

        private void GenerateEntryPoint(SourceProductionContext spc, WorkspaceContext workspaceContext)
        {
            var template = Template.Parse(new TemplateReader().Read("_EntryPointTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(workspaceContext));
            var renderedEntryPoint = template.Render(_templateContext);

            spc.AddSource("_EntryPoint.cs", renderedEntryPoint);
        }

        private void GenerateBasePage(SourceProductionContext spc, WorkspaceContext workspaceContext)
        {
            var template = Template.Parse(new TemplateReader().Read("_BasePageTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(workspaceContext));
            var renderedbasePage = template.Render(_templateContext);

            spc.AddSource("_BasePage.cs", renderedbasePage);
        }

        private void GenerateBaseComponent(SourceProductionContext spc, WorkspaceContext workspaceContext)
        {
            var template = Template.Parse(new TemplateReader().Read("_BaseComponentTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(workspaceContext));
            var renderedbaseComponent = template.Render(_templateContext);

            spc.AddSource("_BaseComponent.cs", renderedbaseComponent);
        }

        private void GenerateSpace(SourceProductionContext spc, SpaceContext spaceContext)
        {
            var template = Template.Parse(new TemplateReader().Read("SpaceTemplate"));

            _templateContext.PushGlobal(ScriptObject.From(spaceContext));
            var renderedSpace = template.Render(_templateContext);

            var generatedFileName = $"{spaceContext.Namespace}.{spaceContext.Name}Space.cs";
            spc.AddSource(generatedFileName, renderedSpace);

            foreach (var space in spaceContext.Spaces)
            {
                GenerateSpace(spc, space);
            }

            foreach (var page in spaceContext.Pages)
            {
                GeneratePage(spc, page);
            }

            foreach (var component in spaceContext.Components)
            {
                GenerateComponent(spc, component);
            }
        }

        private void GeneratePage(SourceProductionContext spc, PageContext pageContext)
        {
            var template = Template.Parse(new TemplateReader().Read("PageTemplate"));

            var scripObject = ScriptObject.From(pageContext);
            scripObject.Import(typeof(Services.GenerationService));
            _templateContext.PushGlobal(scripObject);
            var renderedPage = template.Render(_templateContext);

            var generatedFileName = $"{pageContext.Namespace}.{pageContext.Name}Page.cs";
            spc.AddSource(generatedFileName, renderedPage);
        }

        private void GenerateComponent(SourceProductionContext spc, ComponentContext componentContext)
        {
            var template = Template.Parse(new TemplateReader().Read("ComponentTemplate"));

            var scripObject = ScriptObject.From(componentContext);
            _templateContext.PushGlobal(scripObject);
            var renderedComponent = template.Render(_templateContext);

            var generatedFileName = $"{componentContext.Namespace}.{componentContext.Name}Component.cs";
            spc.AddSource(generatedFileName, renderedComponent);
        }
    }
}

