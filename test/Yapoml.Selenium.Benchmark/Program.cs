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
            workspaceBuilder.AddFile($"{Environment.CurrentDirectory}/MyComponent{i}.pc.yaml",
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
}