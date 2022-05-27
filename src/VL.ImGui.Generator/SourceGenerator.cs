using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VL.ImGui.Generator
{
    [Generator]
    public class SourceGenerator : IIncrementalGenerator
    {
        private const string GenerateImmutableAttributeMetadataName = "VL.ImGui.GenerateNodeAttribute";
        private const string DocumentationAttributeMetadataName = "VL.ImGui.DocumentationAttribute";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var classes = context.SyntaxProvider.CreateSyntaxProvider(
               predicate: (node, token) => node is ClassDeclarationSyntax @class && @class.AttributeLists.Count > 0,
               transform: (ctx, token) => GetSemanticTargetForGeneration(ctx, token))
               .Where(c => c != null);

            var compilationAndClasses = context.CompilationProvider.Combine(classes.Collect());

            context.RegisterSourceOutput(compilationAndClasses, (spc, source) => Execute(source.Left, source.Right, spc));
        }

        private static ClassDeclarationSyntax GetSemanticTargetForGeneration(GeneratorSyntaxContext context, CancellationToken token)
        {
            var classSyntax = (ClassDeclarationSyntax)context.Node;

            foreach (var attributeListSyntax in classSyntax.AttributeLists)
            {
                foreach (var attributeSyntax in attributeListSyntax.Attributes)
                {
                    var attributeSymbol = context.SemanticModel.GetSymbolInfo(attributeSyntax, token).Symbol as IMethodSymbol;
                    if (attributeSymbol is null)
                        continue;

                    var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                    var fullName = attributeContainingTypeSymbol.ToDisplayString();

                    if (fullName == GenerateImmutableAttributeMetadataName)
                        return classSyntax;
                }
            }

            return null;
        }

        private static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext context)
        {
            if (classes.IsDefaultOrEmpty)
                return;

            var attributeSymbol = compilation.GetTypeByMetadataName(GenerateImmutableAttributeMetadataName)
                ?? throw new InvalidOperationException("Symbol not found: " + GenerateImmutableAttributeMetadataName);

            foreach (var syntax in classes.Distinct())
            {
                var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);
                var classSymbol = semanticModel.GetDeclaredSymbol(syntax);
                var attributeData = classSymbol?.GetAttributes().FirstOrDefault(d => d.AttributeClass?.Equals(attributeSymbol, SymbolEqualityComparer.Default) == true);
                var data = attributeData.NamedArguments.ToImmutableDictionary(kv => kv.Key, kv => kv.Value);
                var source = CreateSource(context, syntax, semanticModel, classSymbol, data.GetValueOrDefault("Name").Value as string);
                context.AddSource($"{syntax.Identifier}.g.cs", source);
            }
        }

        private static string CreateSource(SourceProductionContext context, ClassDeclarationSyntax declarationSyntax, SemanticModel semanticModel, INamedTypeSymbol typeSymbol, string name)
        {
            var root = declarationSyntax.SyntaxTree.GetCompilationUnitRoot();
            var declaredUsings = root.Usings;
            foreach (var ns in root.Members.OfType<NamespaceDeclarationSyntax>())
                declaredUsings = declaredUsings.AddRange(ns.Usings);

            var documentationAttributeSymbol = semanticModel.Compilation.GetTypeByMetadataName(DocumentationAttributeMetadataName);

            var indent = "                    ";
            var indent2 = "                        ";
            var inputDescriptions = new List<string>();
            var outputDescriptions = new List<string>();
            var inputs = new List<string>();
            var outputs = new List<string>();
            foreach (var property in typeSymbol.GetMembers().OfType<IPropertySymbol>())
            {
                var attributeData = property.GetAttributes().FirstOrDefault(d => d.AttributeClass.Equals(documentationAttributeSymbol, SymbolEqualityComparer.Default));
                var summary = (attributeData?.ConstructorArguments[0].Value as string)?.Replace("\"", "\\\"");
                if (property.SetMethod != null && property.SetMethod.DeclaredAccessibility == Accessibility.Public)
                {
                    inputDescriptions.Add($"_c.Input(\"{ToUserName(property.Name)}\", _w.{property.Name}, summary: \"{summary}\"),");
                    inputs.Add($"c.Input(v => s.{property.Name} = v, s.{property.Name}),");
                }
                else if (property.GetMethod != null && property.GetMethod.DeclaredAccessibility == Accessibility.Public)
                {
                    outputDescriptions.Add($"_c.Output(\"{ToUserName(property.Name)}\", _w.{property.Name}),");
                    outputs.Add($"c.Output(() => s.{property.Name}),");
                }
            }

            return $@"
using VL.Core;

namespace {typeSymbol.ContainingNamespace}
{{
    partial class {typeSymbol.Name}
    {{
        internal static IVLNodeDescription GetNodeDescription(IVLNodeDescriptionFactory factory)
        {{
            return factory.NewNodeDescription(""{name ?? typeSymbol.Name}"", ""ImGui"", fragmented: true, _c =>
            {{
                var _w = new {typeSymbol.Name}();
                var _inputs = new[]
                {{
                    _c.Input(""Input"", _w.Input),
                    { string.Join($"{Environment.NewLine}{indent}", inputDescriptions)}
                }};
                var _outputs = new[]
                {{
                    _c.Output<Widget>(""Output""),
                    {string.Join($"{Environment.NewLine}{indent}", outputDescriptions)}
                }};
                return _c.NewNode(_inputs, _outputs, c =>
                {{
                    var s = new {typeSymbol.Name}();
                    var inputs = new IVLPin[]
                    {{
                        c.Input(v => s.Input = v, s.Input),
                        {string.Join($"{Environment.NewLine}{indent2}", inputs)}
                    }};
                    var outputs = new IVLPin[]
                    {{
                        c.Output(() => s),
                        {string.Join($"{Environment.NewLine}{indent2}", outputs)}
                    }};
                    return c.Node(inputs, outputs);
                }});
            }});
        }}
    }}
}}
";
        }

        // Doesn't work :( It seems VS is not passing that information in
        private static string GetDocEntry(ISymbol symbol, string tag, string name = null)
        {
            try
            {
                var rawComment = symbol.GetDocumentationCommentXml();
                if (string.IsNullOrWhiteSpace(rawComment))
                    return null;

                var x = XElement.Parse($"<X>{rawComment}</X>");
                if (name != null)
                    return x.Elements(tag).FirstOrDefault(e => e.Attribute("name")?.Value == name)?.ToString();
                else
                    return x.Element(tag)?.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        static readonly Regex FCamelCasePattern = new Regex("[a-z][A-Z0-9]", RegexOptions.Compiled);

        private static string ToUserName(string name)
        {
            var userName = FCamelCasePattern.Replace(name, match => $"{match.Value[0]} {match.Value[1]}");
            if (userName.Length > 0)
                return char.ToUpper(userName[0]) + userName.Substring(1);
            return name;
        }

        private class Progress : IProgress<Diagnostic>
        {
            private readonly SourceProductionContext context;

            public Progress(SourceProductionContext context)
            {
                this.context = context;
            }

            public void Report(Diagnostic value)
            {
                context.ReportDiagnostic(value);
            }
        }
    }
}
