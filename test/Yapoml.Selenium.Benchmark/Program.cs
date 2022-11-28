using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Yapoml.Framework.Workspace;
using Yapoml.Framework.Workspace.Parsers;
using Yapoml.Selenium.SourceGeneration;

BenchmarkRunner.Run<Benchmarks>();

[SimpleJob, MemoryDiagnoser]
public class Benchmarks
{
    WorkspaceContext _workspace;

    SourceProducer _sourceProducer;

    [Params(50, 500)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        var workspaceBuilder = new WorkspaceContextBuilder(Environment.CurrentDirectory, "A.B", new WorkspaceParser());

        for (int i = 0; i < N; i++)
        {
            workspaceBuilder.AddFile($"{Environment.CurrentDirectory}/MyComponent{i}.component.yaml",
                @"
SomeButton: ./qwe
"
                );
        }

        _workspace = workspaceBuilder.Build();

        _sourceProducer = new SourceProducer();
    }

    [Benchmark]
    public void GenerateComponents()
    {
        foreach (var c in _workspace.Components)
        {
            _sourceProducer.ProduceComponent(c);
        }
    }

    [Benchmark]
    public void GenerateComponentsIncludingParsing()
    {
        var workspaceBuilder = new WorkspaceContextBuilder(Environment.CurrentDirectory, "A.B", new WorkspaceParser());

        for (int i = 0; i < N; i++)
        {
            workspaceBuilder.AddFile($"{Environment.CurrentDirectory}/MyComponent{i}.pc.yaml",
                @"
SomeButton: ./qwe
"
                );
        }

        var workspace = workspaceBuilder.Build();

        var sourceProducer = new SourceProducer();

        foreach (var c in workspace.Components)
        {
            sourceProducer.ProduceComponent(c);
        }
    }
}