using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Yapoml.Framework.Workspace;
using Yapoml.Framework.Workspace.Parsers;

[assembly: InternalsVisibleTo("Yapoml.Selenium.Test")]
[assembly: InternalsVisibleTo("Yapoml.Selenium.Benchmark")]

namespace Yapoml.Selenium.Generation
{
    [Generator]
    internal class Generator : IIncrementalGenerator
    {
        private string _rootNamespace;
        private string _projectDir;

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var globalOptions = context.AnalyzerConfigOptionsProvider.Select((o, c) => o.GlobalOptions);
            context.RegisterSourceOutput(globalOptions, (c, s) =>
            {
                s.TryGetValue("build_property.RootNamespace", out _rootNamespace);
                s.TryGetValue("build_property.ProjectDir", out _projectDir);
            });

            var textFiles = context.AdditionalTextsProvider.Where(file => file.Path.EndsWith(".po.yaml", StringComparison.OrdinalIgnoreCase) || file.Path.EndsWith(".pc.yaml", StringComparison.OrdinalIgnoreCase)).Collect();

            context.RegisterSourceOutput(textFiles, (spc, files) =>
            {
                var sw = Stopwatch.StartNew();

                try
                {
                    var sourceProducer = new SourceProducer();

                    var parser = new WorkspaceParser();

                    // build yapoml generation context
                    var yaContextBuilder = new WorkspaceContextBuilder(_projectDir, _rootNamespace, parser);

                    var parsingSw = Stopwatch.StartNew();

                    foreach (var file in files)
                    {
                        spc.CancellationToken.ThrowIfCancellationRequested();

                        yaContextBuilder.AddFile(file.Path, file.GetText(spc.CancellationToken).ToString());
                    }

                    var yaContext = yaContextBuilder.Build();

                    // generate files
                    if (yaContext.Spaces.Any() || yaContext.Pages.Any() || yaContext.Components.Any())
                    {
                        var tasks = new List<Task>();

                        GenerateEntryPoint(spc, yaContext, sourceProducer);
                        GenerateBasePage(spc, yaContext, sourceProducer);
                        GenerateBaseComponent(spc, yaContext, sourceProducer);

                        foreach (var space in yaContext.Spaces)
                        {
                            foreach (var (file, content) in GenerateSpace(spc, space, sourceProducer))
                            {
                                spc.AddSource(file, content);
                            }
                        }

                        foreach (var page in yaContext.Pages)
                        {
                            var (file, content) = GeneratePage(spc, page, sourceProducer);

                            spc.AddSource(file, content);
                        }

                        foreach (var component in yaContext.Components)
                        {
                            var (file, content) = GenerateComponent(spc, component, sourceProducer);

                            spc.AddSource(file, content);
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

        private void GenerateEntryPoint(SourceProductionContext spc, WorkspaceContext workspaceContext, SourceProducer sourceProducer)
        {
            spc.CancellationToken.ThrowIfCancellationRequested();

            spc.AddSource("_EntryPoint.cs", sourceProducer.ProduceEntryPoint(workspaceContext));
        }

        private void GenerateBasePage(SourceProductionContext spc, WorkspaceContext workspaceContext, SourceProducer sourceProducer)
        {
            spc.CancellationToken.ThrowIfCancellationRequested();

            spc.AddSource("_BasePage.cs", sourceProducer.ProduceBasePage(workspaceContext));
        }

        private void GenerateBaseComponent(SourceProductionContext spc, WorkspaceContext workspaceContext, SourceProducer sourceProducer)
        {
            spc.CancellationToken.ThrowIfCancellationRequested();

            spc.AddSource("_BaseComponent.cs", sourceProducer.ProduceBaseComponent(workspaceContext));
        }

        private IList<(string file, string content)> GenerateSpace(SourceProductionContext spc, SpaceContext spaceContext, SourceProducer sourceProducer)
        {
            var files = new List<(string file, string content)>();

            spc.CancellationToken.ThrowIfCancellationRequested();

            var generatedFileName = $"{spaceContext.Namespace}.{spaceContext.Name}Space.cs";

            files.Add((generatedFileName, sourceProducer.ProduceSpace(spaceContext)));

            foreach (var space in spaceContext.Spaces)
            {
                files.AddRange(GenerateSpace(spc, space, sourceProducer));
            }

            foreach (var page in spaceContext.Pages)
            {
                files.Add(GeneratePage(spc, page, sourceProducer));
            }

            foreach (var component in spaceContext.Components)
            {
                files.Add(GenerateComponent(spc, component, sourceProducer));
            }

            return files;
        }

        private (string file, string content) GeneratePage(SourceProductionContext spc, PageContext pageContext, SourceProducer sourceProducer)
        {
            spc.CancellationToken.ThrowIfCancellationRequested();

            var generatedFileName = $"{pageContext.Namespace}.{pageContext.Name}Page.cs";

            return (generatedFileName, sourceProducer.ProducePage(pageContext));
        }

        private (string file, string content) GenerateComponent(SourceProductionContext spc, ComponentContext componentContext, SourceProducer sourceProducer)
        {
            spc.CancellationToken.ThrowIfCancellationRequested();

            var generatedFileName = $"{componentContext.Namespace}.{componentContext.Name}Component.cs";
            return (generatedFileName, sourceProducer.ProduceComponent(componentContext));
        }
    }
}

