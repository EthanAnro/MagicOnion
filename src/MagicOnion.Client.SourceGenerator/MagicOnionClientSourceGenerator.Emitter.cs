using System.Collections.Immutable;
using MagicOnion.Client.SourceGenerator.CodeAnalysis;
using MagicOnion.Client.SourceGenerator.CodeGen;
using MagicOnion.Client.SourceGenerator.CodeGen.MemoryPack;
using MagicOnion.Client.SourceGenerator.CodeGen.MessagePack;
using Microsoft.CodeAnalysis;

namespace MagicOnion.Client.SourceGenerator;

public partial class MagicOnionClientSourceGenerator
{
    public const string SourceGeneratorAttributesHintName = "MagicOnionClientSourceGeneratorAttributes.g.cs";
    public const string MagicOnionClientGenerationAttributeShortName = "MagicOnionClientGeneration";
    public const string MagicOnionClientGenerationAttributeName = $"{MagicOnionClientGenerationAttributeShortName}Attribute";
    public const string MagicOnionClientGenerationAttributeFullName = $"MagicOnion.Client.{MagicOnionClientGenerationAttributeName}";

    static class Emitter
    {
        public static void AddAttributeSources(Action<string, string> addSource)
        {
            addSource(SourceGeneratorAttributesHintName, $$"""
                // <auto-generated />
                namespace MagicOnion.Client
                {
                    /// <summary>
                    /// Marker attribute for generating clients of MagicOnion.
                    /// The source generator collects the classes specified by this attribute and uses them to generate source.
                    /// </summary>
                    [global::System.Diagnostics.Conditional("__MagicOnion_Client_SourceGenerator__DesignTimeOnly__")]
                    [global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false)]
                    internal class {{MagicOnionClientGenerationAttributeName}} : global::System.Attribute
                    {
                        /// <summary>
                        /// Gets or sets whether to disable automatically calling `Register` during start-up. (Automatic registration requires .NET 5+ or Unity)
                        /// </summary>
                        public bool DisableAutoRegistration { get; set; }

                        /// <summary>
                        /// Gets or set the serializer used for message serialization. The default value is <see cref="GenerateSerializerType.MessagePack"/>.
                        /// </summary>
                        public global::MagicOnion.Client.GenerateSerializerType Serializer { get; set; } = global::MagicOnion.Client.GenerateSerializerType.MessagePack;

                        /// <summary>
                        /// Gets or set the namespace of pre-generated MessagePackFormatters. The default value is <c>MessagePack.Formatters</c>.
                        /// </summary>
                        public string MessagePackFormatterNamespace { get; set; } = "MessagePack.Formatters";
                
                        /// <summary>
                        /// Gets or set whether to enable the StreamingHandler diagnostic handler. This is for debugging purpose. The default value is <see langword="false" />.
                        /// </summary>
                        public bool EnableStreamingHubDiagnosticHandler { get; set; } = false;

                        public global::System.Type[] TypesContainedInTargetAssembly { get; }
                
                        /// <param name="typesContainedInTargetAssembly">Types contained in the scan target assembly</param>
                        public {{MagicOnionClientGenerationAttributeName}}(params global::System.Type[] typesContainedInTargetAssembly)
                        {
                            TypesContainedInTargetAssembly = typesContainedInTargetAssembly;
                        }
                    }

                    // This enum must be mirror of `SerializerType` (MagicOnionClientSourceGenerator)
                    internal enum GenerateSerializerType
                    {
                        MessagePack = 0,
                        MemoryPack = 1,
                    }
                }
                """);
        }

        public static void Emit(GenerationContext context, ImmutableArray<INamedTypeSymbol> interfaceSymbols, ReferenceSymbols referenceSymbols)
        {
            var (serviceCollection, diagnostics) = MethodCollector.Collect(interfaceSymbols, referenceSymbols, context.SourceProductionContext.CancellationToken);
            var generated = EmitCore(context, serviceCollection, context.SourceProductionContext.CancellationToken);

            foreach (var diagnostic in diagnostics)
            {
                context.SourceProductionContext.ReportDiagnostic(diagnostic);
            }

            foreach (var (path, source) in generated)
            {
                context.SourceProductionContext.AddSource(path, source);
            }
        }

        static IReadOnlyList<(string Path, string Source)> EmitCore(GenerationContext context, MagicOnionServiceCollection serviceCollection, CancellationToken cancellationToken)
        {
            var outputs = new List<(string Path, string Source)>();

            // Configure serialization
            (ISerializationFormatterNameMapper Mapper, ISerializerFormatterGenerator Generator, Func<IEnumerable<EnumSerializationInfo>, string> EnumFormatterGenerator)
                serialization = context.Options.Serializer switch
            {
                SerializerType.MemoryPack => (
                    Mapper: new MemoryPackFormatterNameMapper(),
                    Generator: new MemoryPackFormatterRegistrationGenerator(),
                    EnumFormatterGenerator: _ => string.Empty
                ),
                SerializerType.MessagePack => (
                    Mapper: new MessagePackFormatterNameMapper(context.Options.MessagePackFormatterNamespace),
                    Generator: new MessagePackFormatterResolverGenerator(),
                    EnumFormatterGenerator: x => MessagePackEnumFormatterGenerator.Build(context, x)
                ),
                _ => throw new NotImplementedException(),
            };

            cancellationToken.ThrowIfCancellationRequested();

            var serializationInfoCollector = new SerializationInfoCollector(serialization.Mapper);
            var serializationInfoCollection = serializationInfoCollector.Collect(serviceCollection);

            cancellationToken.ThrowIfCancellationRequested();

            var formatterCodeGenContext = new SerializationFormatterCodeGenContext(context.Namespace ?? string.Empty, serializationInfoCollection.RequireRegistrationFormatters, serializationInfoCollection.TypeHints);
            var (serializerHintNameSuffix, serializationSource) = serialization.Generator.Build(context, formatterCodeGenContext);

            cancellationToken.ThrowIfCancellationRequested();

            outputs.Add((GeneratePathFromNamespaceAndTypeName(context.Namespace ?? string.Empty, context.InitializerPartialTypeName), MagicOnionInitializerGenerator.Build(context, serviceCollection)));
            outputs.Add((GeneratePathFromNamespaceAndTypeName(context.Namespace ?? string.Empty, context.InitializerPartialTypeName + serializerHintNameSuffix), serializationSource));

            if (serializationInfoCollection.Enums.Any())
            {
                outputs.Add((GeneratePathFromNamespaceAndTypeName(context.Namespace ?? string.Empty, context.InitializerPartialTypeName + ".EnumFormatters"), serialization.EnumFormatterGenerator(serializationInfoCollection.Enums)));
            }

            foreach (var service in serviceCollection.Services)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var x = StaticMagicOnionClientGenerator.Build(context, service);
                outputs.Add((GeneratePathFromNamespaceAndTypeName(service.ServiceType.Namespace, service.GetClientName()), x));
            }

            foreach (var hub in serviceCollection.Hubs)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var x = StaticStreamingHubClientGenerator.Build(context, hub);
                outputs.Add((GeneratePathFromNamespaceAndTypeName(hub.ServiceType.Namespace, hub.GetClientName()), x));
            }

            return outputs.OrderBy(x => x.Path).ToArray();
        }

        static string GeneratePathFromNamespaceAndTypeName(string ns, string className)
        {
            return $"{ns}_{className}".Replace(".", "_").Replace("global::", string.Empty) + ".g.cs";
        }
    }
}
