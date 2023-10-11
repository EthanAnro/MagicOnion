﻿// <auto-generated />
#pragma warning disable CS0618 // 'member' is obsolete: 'text'
#pragma warning disable CS0612 // 'member' is obsolete
#pragma warning disable CS0414 // The private field 'field' is assigned but its value is never used

namespace TempProject
{
    using global::System;
    using global::Grpc.Core;
    using global::MagicOnion;
    using global::MagicOnion.Client;
    using global::MessagePack;
    
    [global::MagicOnion.Ignore]
    public class MyHubClient : global::MagicOnion.Client.StreamingHubClientBase<global::TempProject.IMyHub, global::TempProject.IMyHubReceiver>, global::TempProject.IMyHub
    {
        protected override global::Grpc.Core.Method<global::System.Byte[], global::System.Byte[]> DuplexStreamingAsyncMethod { get; }
        
        public MyHubClient(global::Grpc.Core.CallInvoker callInvoker, global::System.String host, global::Grpc.Core.CallOptions options, global::MagicOnion.Serialization.IMagicOnionSerializerProvider serializerProvider, global::MagicOnion.Client.IMagicOnionClientLogger logger)
            : base(callInvoker, host, options, serializerProvider, logger)
        {
            var marshaller = global::MagicOnion.MagicOnionMarshallers.ThroughMarshaller;
            DuplexStreamingAsyncMethod = new global::Grpc.Core.Method<global::System.Byte[], global::System.Byte[]>(global::Grpc.Core.MethodType.DuplexStreaming, "IMyHub", "Connect", marshaller, marshaller);
        }
        
        public global::System.Threading.Tasks.Task<global::System.String[]> GetStringValuesAsync()
            => base.WriteMessageWithResponseAsync<global::MessagePack.Nil, global::System.String[]>(1774317884, global::MessagePack.Nil.Default);
        public global::System.Threading.Tasks.Task<global::System.Int32[]> GetIntValuesAsync()
            => base.WriteMessageWithResponseAsync<global::MessagePack.Nil, global::System.Int32[]>(-400881550, global::MessagePack.Nil.Default);
        public global::System.Threading.Tasks.Task<global::System.Int32[]> GetInt32ValuesAsync()
            => base.WriteMessageWithResponseAsync<global::MessagePack.Nil, global::System.Int32[]>(309063297, global::MessagePack.Nil.Default);
        public global::System.Threading.Tasks.Task<global::System.Single[]> GetSingleValuesAsync()
            => base.WriteMessageWithResponseAsync<global::MessagePack.Nil, global::System.Single[]>(702446639, global::MessagePack.Nil.Default);
        public global::System.Threading.Tasks.Task<global::System.Boolean[]> GetBooleanValuesAsync()
            => base.WriteMessageWithResponseAsync<global::MessagePack.Nil, global::System.Boolean[]>(2082077357, global::MessagePack.Nil.Default);
        
        public global::TempProject.IMyHub FireAndForget()
            => new FireAndForgetClient(this);
        
        [global::MagicOnion.Ignore]
        class FireAndForgetClient : global::TempProject.IMyHub
        {
            readonly MyHubClient parent;
        
            public FireAndForgetClient(MyHubClient parent)
                => this.parent = parent;
        
            public global::TempProject.IMyHub FireAndForget() => this;
            public global::System.Threading.Tasks.Task DisposeAsync() => throw new global::System.NotSupportedException();
            public global::System.Threading.Tasks.Task WaitForDisconnect() => throw new global::System.NotSupportedException();
        
            public global::System.Threading.Tasks.Task<global::System.String[]> GetStringValuesAsync()
                => parent.WriteMessageFireAndForgetAsync<global::MessagePack.Nil, global::System.String[]>(1774317884, global::MessagePack.Nil.Default);
            public global::System.Threading.Tasks.Task<global::System.Int32[]> GetIntValuesAsync()
                => parent.WriteMessageFireAndForgetAsync<global::MessagePack.Nil, global::System.Int32[]>(-400881550, global::MessagePack.Nil.Default);
            public global::System.Threading.Tasks.Task<global::System.Int32[]> GetInt32ValuesAsync()
                => parent.WriteMessageFireAndForgetAsync<global::MessagePack.Nil, global::System.Int32[]>(309063297, global::MessagePack.Nil.Default);
            public global::System.Threading.Tasks.Task<global::System.Single[]> GetSingleValuesAsync()
                => parent.WriteMessageFireAndForgetAsync<global::MessagePack.Nil, global::System.Single[]>(702446639, global::MessagePack.Nil.Default);
            public global::System.Threading.Tasks.Task<global::System.Boolean[]> GetBooleanValuesAsync()
                => parent.WriteMessageFireAndForgetAsync<global::MessagePack.Nil, global::System.Boolean[]>(2082077357, global::MessagePack.Nil.Default);
            
        }
        
        protected override void OnBroadcastEvent(global::System.Int32 methodId, global::System.ArraySegment<global::System.Byte> data)
        {
            switch (methodId)
            {
            }
        }
        
        protected override void OnResponseEvent(global::System.Int32 methodId, global::System.Object taskCompletionSource, global::System.ArraySegment<global::System.Byte> data)
        {
            switch (methodId)
            {
                case 1774317884: // Task<String[]> GetStringValuesAsync()
                    base.SetResultForResponse<global::System.String[]>(taskCompletionSource, data);
                    break;
                case -400881550: // Task<Int32[]> GetIntValuesAsync()
                    base.SetResultForResponse<global::System.Int32[]>(taskCompletionSource, data);
                    break;
                case 309063297: // Task<Int32[]> GetInt32ValuesAsync()
                    base.SetResultForResponse<global::System.Int32[]>(taskCompletionSource, data);
                    break;
                case 702446639: // Task<Single[]> GetSingleValuesAsync()
                    base.SetResultForResponse<global::System.Single[]>(taskCompletionSource, data);
                    break;
                case 2082077357: // Task<Boolean[]> GetBooleanValuesAsync()
                    base.SetResultForResponse<global::System.Boolean[]>(taskCompletionSource, data);
                    break;
            }
        }
        
    }
}

