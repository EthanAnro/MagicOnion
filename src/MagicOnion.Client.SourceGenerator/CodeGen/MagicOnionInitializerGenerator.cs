using MagicOnion.Client.SourceGenerator.CodeAnalysis;

namespace MagicOnion.Client.SourceGenerator.CodeGen;

internal class MagicOnionInitializerGenerator
{
    public static string Build(GenerationContext generationContext, MagicOnionServiceCollection serviceCollection)
    {
        using var pooledStringBuilder = generationContext.GetPooledStringBuilder();
        var writer = pooledStringBuilder.Instance;

        writer.AppendLine($$"""
            // <auto-generated />
            #pragma warning disable CS0618 // 'member' is obsolete: 'text'
            #pragma warning disable CS0612 // 'member' is obsolete
            #pragma warning disable CS8019 // Unnecessary using directive.
            """);
        if (!string.IsNullOrEmpty(generationContext.Namespace))
        {
            writer.AppendLineWithFormat($$"""
            namespace {{generationContext.Namespace}}
            {
            """);
        }

        writer.AppendLineWithFormat($$"""
                using global::System;
                using global::System.Collections.Generic;
                using global::System.Linq;
                using global::MagicOnion;
                using global::MagicOnion.Client;

                partial class PreserveAttribute : global::System.Attribute {}
            
                partial class {{generationContext.InitializerPartialTypeName}}
                {
                    static bool isRegistered = false;
                    readonly static MagicOnionGeneratedClientFactoryProvider provider = new();

                    /// <summary>
                    /// Gets the generated MagicOnionClientFactoryProvider.
                    /// </summary>
                    public static global::MagicOnion.Client.IMagicOnionClientFactoryProvider ClientFactoryProvider => provider;

                    /// <summary>
                    /// Gets the generated StreamingHubClientFactoryProvider.
                    /// </summary>
                    public static global::MagicOnion.Client.IStreamingHubClientFactoryProvider StreamingHubClientFactoryProvider => provider;
            """);

        if (!generationContext.Options.DisableAutoRegistration)
        {
            writer.AppendLineWithFormat($$"""
            #if UNITY_2019_4_OR_NEWER
                    [global::UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
            #elif NET5_0_OR_GREATER
                    [global::System.Runtime.CompilerServices.ModuleInitializer]
            #endif
                    internal static void Register() => TryRegisterProviderFactory();
            """);
        }

        if (generationContext.Options.EnableStreamingHubDiagnosticHandler)
        {
            writer.AppendLineWithFormat($$"""
                    /// <summary>
                    /// Gets or sets a diagnostic handler for the StreamingHub.
                    /// </summary>
                    public static global::MagicOnion.Client.IStreamingHubDiagnosticHandler? StreamingHubDiagnosticHandler { get; set; }
            """);
        }

        writer.AppendLineWithFormat($$"""

                    /// <summary>
                    /// Register the generated client factory providers if it's not registered yet. This method will register only once.
                    /// </summary>
                    public static bool TryRegisterProviderFactory()
                    {
                        if (isRegistered) return false;
                        isRegistered = true;

                        global::MagicOnion.Client.MagicOnionClientFactoryProvider.Default =
                            (global::MagicOnion.Client.MagicOnionClientFactoryProvider.Default is global::MagicOnion.Client.ImmutableMagicOnionClientFactoryProvider immutableMagicOnionClientFactoryProvider)
                                ? immutableMagicOnionClientFactoryProvider.Add(provider)
                                : new global::MagicOnion.Client.ImmutableMagicOnionClientFactoryProvider(provider);

                        global::MagicOnion.Client.StreamingHubClientFactoryProvider.Default =
                            (global::MagicOnion.Client.StreamingHubClientFactoryProvider.Default is global::MagicOnion.Client.ImmutableStreamingHubClientFactoryProvider immutableStreamingHubClientFactoryProvider)
                                ? immutableStreamingHubClientFactoryProvider.Add(provider)
                                : new global::MagicOnion.Client.ImmutableStreamingHubClientFactoryProvider(provider);

                        return true;
                    }

                    class MagicOnionGeneratedClientFactoryProvider : global::MagicOnion.Client.IMagicOnionClientFactoryProvider, global::MagicOnion.Client.IStreamingHubClientFactoryProvider
                    {
                        bool global::MagicOnion.Client.IMagicOnionClientFactoryProvider.TryGetFactory<T>(out global::MagicOnion.Client.MagicOnionClientFactoryDelegate<T> factory)
                            => (factory = MagicOnionClientFactoryCache<T>.Factory) != null;

                        bool global::MagicOnion.Client.IStreamingHubClientFactoryProvider.TryGetFactory<TStreamingHub, TReceiver>(out global::MagicOnion.Client.StreamingHubClientFactoryDelegate<TStreamingHub, TReceiver> factory)
                            => (factory = StreamingHubClientFactoryCache<TStreamingHub, TReceiver>.Factory) != null;

                        static class MagicOnionClientFactoryCache<T> where T : global::MagicOnion.IService<T>
                        {
                            public readonly static global::MagicOnion.Client.MagicOnionClientFactoryDelegate<T> Factory;

                            static MagicOnionClientFactoryCache()
                            {
                                object factory = default(global::MagicOnion.Client.MagicOnionClientFactoryDelegate<T>);

            """);

        foreach (var serviceInfo in serviceCollection.Services)
        {
           writer.AppendLineWithFormat($$"""
                                if (typeof(T) == typeof({{serviceInfo.ServiceType.FullName}}))
                                {
                                    factory = ((global::MagicOnion.Client.MagicOnionClientFactoryDelegate<{{serviceInfo.ServiceType.FullName}}>)((x, y) => new MagicOnionGeneratedClient.{{serviceInfo.GetClientFullName()}}(x, y)));
                                }
            """);
        }
        
        writer.AppendLineWithFormat($$"""
                                Factory = (global::MagicOnion.Client.MagicOnionClientFactoryDelegate<T>)factory;
                            }
                        }
                        
                        static class StreamingHubClientFactoryCache<TStreamingHub, TReceiver> where TStreamingHub : global::MagicOnion.IStreamingHub<TStreamingHub, TReceiver>
                        {
                            public readonly static global::MagicOnion.Client.StreamingHubClientFactoryDelegate<TStreamingHub, TReceiver> Factory;

                            static StreamingHubClientFactoryCache()
                            {
                                object factory = default(global::MagicOnion.Client.StreamingHubClientFactoryDelegate<TStreamingHub, TReceiver>);

            """);
        foreach (var hubInfo in serviceCollection.Hubs)
        {
            if (generationContext.Options.EnableStreamingHubDiagnosticHandler)
            {
                writer.AppendLineWithFormat($$"""
                                if (typeof(TStreamingHub) == typeof({{hubInfo.ServiceType.FullName}}) && typeof(TReceiver) == typeof({{hubInfo.Receiver.ReceiverType.FullName}}))
                                {
                                    factory = ((global::MagicOnion.Client.StreamingHubClientFactoryDelegate<{{hubInfo.ServiceType.FullName}}, {{hubInfo.Receiver.ReceiverType.FullName}}>)((a, _, b, c, d, e) => new MagicOnionGeneratedClient.{{hubInfo.GetClientFullName()}}(a, b, c, d, e, StreamingHubDiagnosticHandler)));
                                }
            """);
            }
            else
            {
                writer.AppendLineWithFormat($$"""
                                if (typeof(TStreamingHub) == typeof({{hubInfo.ServiceType.FullName}}) && typeof(TReceiver) == typeof({{hubInfo.Receiver.ReceiverType.FullName}}))
                                {
                                    factory = ((global::MagicOnion.Client.StreamingHubClientFactoryDelegate<{{hubInfo.ServiceType.FullName}}, {{hubInfo.Receiver.ReceiverType.FullName}}>)((a, _, b, c, d, e) => new MagicOnionGeneratedClient.{{hubInfo.GetClientFullName()}}(a, b, c, d, e)));
                                }
            """);
            }
        }

        writer.AppendLineWithFormat($$$"""

                                Factory = (global::MagicOnion.Client.StreamingHubClientFactoryDelegate<TStreamingHub, TReceiver>)factory;
                            }
                        }
                    }
                }
            """);
        if (!string.IsNullOrEmpty(generationContext.Namespace))
        {
            writer.AppendLineWithFormat($$"""
            }
            """);
        }

        return writer.ToString();
    }
}
