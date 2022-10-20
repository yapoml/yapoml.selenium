using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Yapoml.Selenium.SourceGeneration;

namespace Yapoml.Selenium.Test
{
    public class GenerationFixture
    {
        [Test]
        public void Test1()
        {
            Generator generator = new Generator();

            var list = new List<MyAdditionalText>();

            for (int i = 0; i < 50; i++)
            {
                list.Add(new MyAdditionalText(Environment.CurrentDirectory + $"/MyComponent{i}.pc.yaml",
@"
SomeButton: ./qwe
"
                    ));
            }

            var files = ImmutableArray.Create<AdditionalText>(list.ToArray());

            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator)
                .WithUpdatedAnalyzerConfigOptions(new MyConfigOptionsProvider())
                .AddAdditionalTexts(files);

            var inputCompilation = CreateCompilation();

            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            GeneratorDriverRunResult runResult = driver.GetRunResult();

            runResult.Diagnostics.Should().BeEmpty();
            runResult.GeneratedTrees.Should().HaveCount(53);
        }

        private static Compilation CreateCompilation()
            => CSharpCompilation.Create("compilation",
                null,
                new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
                new CSharpCompilationOptions(OutputKind.ConsoleApplication));

        private class MyConfigOptionsProvider : AnalyzerConfigOptionsProvider
        {
            public override AnalyzerConfigOptions GlobalOptions => new MyAnalyzerConfigOptions();

            public override AnalyzerConfigOptions GetOptions(SyntaxTree tree)
            {
                throw new NotImplementedException();
            }

            public override AnalyzerConfigOptions GetOptions(AdditionalText textFile)
            {
                throw new NotImplementedException();
            }
        }

        private class MyAnalyzerConfigOptions : AnalyzerConfigOptions
        {
            private Dictionary<string, string> _options = new Dictionary<string, string> {
                { "build_property.RootNamespace", "A.B" },
                { "build_property.ProjectDir", Environment.CurrentDirectory } };

            public override bool TryGetValue(string key, [NotNullWhen(true)] out string? value)
            {
                return _options.TryGetValue(key, out value);
            }
        }

        private class MyAdditionalText : AdditionalText
        {
            private string _path;
            private readonly string _content;

            public MyAdditionalText(string path, string content)
            {
                _path = path;
                _content = content;
            }

            public override string Path => _path;

            public override SourceText? GetText(CancellationToken cancellationToken = default)
            {
                return SourceText.From(_content);
            }
        }

    }
}