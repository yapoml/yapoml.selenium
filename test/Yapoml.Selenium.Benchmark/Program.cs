using System.Diagnostics;
using Yapoml.Framework.Workspace;
using Yapoml.Framework.Workspace.Parsers;
using Yapoml.Selenium.Generation;

var workspace = new WorkspaceContextBuilder(Environment.CurrentDirectory, "A.B", new WorkspaceParser());

for (int i = 0; i < 50; i++)
{
    workspace.AddFile($"{Environment.CurrentDirectory}/MyComponent{i}.pc.yaml",
        @"
SomeButton: ./qwe
"
        );
}

var w = workspace.Build();

var sourceProducer = new SourceProducer();

var sw = Stopwatch.StartNew();

foreach (var c in w.Components)
{
    sourceProducer.ProduceComponent(c);
}

Console.WriteLine(sw.Elapsed);
