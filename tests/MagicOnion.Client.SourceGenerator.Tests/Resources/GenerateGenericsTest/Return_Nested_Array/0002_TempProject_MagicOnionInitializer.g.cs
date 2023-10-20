﻿// <auto-generated />
#pragma warning disable CS0618 // 'member' is obsolete: 'text'
#pragma warning disable CS0612 // 'member' is obsolete
#pragma warning disable CS8019 // Unnecessary using directive.
namespace TempProject
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::MagicOnion;
    using global::MagicOnion.Client;

    partial class PreserveAttribute : global::System.Attribute {}

    partial class MagicOnionInitializer
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
#if UNITY_2019_4_OR_NEWER
        [global::UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#elif NET5_0_OR_GREATER
        [global::System.Runtime.CompilerServices.ModuleInitializer]
#endif
        internal static void Register() => TryRegisterProviderFactory();

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

                    if (typeof(T) == typeof(global::TempProject.IMyService))
                    {
                        factory = ((global::MagicOnion.Client.MagicOnionClientFactoryDelegate<global::TempProject.IMyService>)((x, y) => new MagicOnionGeneratedClient.TempProject_MyServiceClient(x, y)));
                    }
                    Factory = (global::MagicOnion.Client.MagicOnionClientFactoryDelegate<T>)factory;
                }
            }
            
            static class StreamingHubClientFactoryCache<TStreamingHub, TReceiver> where TStreamingHub : global::MagicOnion.IStreamingHub<TStreamingHub, TReceiver>
            {
                public readonly static global::MagicOnion.Client.StreamingHubClientFactoryDelegate<TStreamingHub, TReceiver> Factory;

                static StreamingHubClientFactoryCache()
                {
                    object factory = default(global::MagicOnion.Client.StreamingHubClientFactoryDelegate<TStreamingHub, TReceiver>);


                    Factory = (global::MagicOnion.Client.StreamingHubClientFactoryDelegate<TStreamingHub, TReceiver>)factory;
                }
            }
        }
    }
}
