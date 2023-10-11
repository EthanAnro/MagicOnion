﻿// <auto-generated />
#pragma warning disable CS0618 // 'member' is obsolete: 'text'
#pragma warning disable CS0612 // 'member' is obsolete

namespace MagicOnion.Resolvers
{
    using global::System;
    using global::MessagePack;

    partial class PreserveAttribute : global::System.Attribute {}
    public class MagicOnionResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new MagicOnionResolver();
    
        MagicOnionResolver() {}
    
        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
            => FormatterCache<T>.formatter;
    
        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;
    
            static FormatterCache()
            {
                var f = MagicOnionResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }
    internal static class MagicOnionResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<global::System.Type, int> lookup;
    
        static MagicOnionResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(1)
            {
                {typeof(global::MagicOnion.DynamicArgumentTuple<global::System.String, global::System.Int32, global::System.Boolean>), 0 },
            };
        }
        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }
        
            switch (key)
            {
                case 0: return new global::MagicOnion.DynamicArgumentTupleFormatter<global::System.String, global::System.Int32, global::System.Boolean>(default(global::System.String), default(global::System.Int32), default(global::System.Boolean));
                default: return null;
            }
        }
    }
    /// <summary>Type hints for Ahead-of-Time compilation.</summary>
    [MagicOnion.Resolvers.Preserve]
    internal static class TypeHints
    {
        [MagicOnion.Resolvers.Preserve]
        internal static void Register()
        {
            _ = MagicOnionResolver.Instance.GetFormatter<global::MagicOnion.DynamicArgumentTuple<global::System.String, global::System.Int32, global::System.Boolean>>();
            _ = MagicOnionResolver.Instance.GetFormatter<global::MessagePack.Nil>();
            _ = MagicOnionResolver.Instance.GetFormatter<global::System.Boolean>();
            _ = MagicOnionResolver.Instance.GetFormatter<global::System.Int32>();
            _ = MagicOnionResolver.Instance.GetFormatter<global::System.String>();
        }
    }
}
