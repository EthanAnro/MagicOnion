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
        
            
        }
        
        protected override void OnBroadcastEvent(global::System.Int32 methodId, global::System.ArraySegment<global::System.Byte> data)
        {
            switch (methodId)
            {
                case -1005848884: // Void A(global::TempProject.MyGenericObject<global::TempProject.MyEnum> a)
                    {
                        var value = base.Deserialize<global::TempProject.MyGenericObject<global::TempProject.MyEnum>>(data);
                        receiver.A(value);
                    }
                    break;
            }
        }
        
        protected override void OnResponseEvent(global::System.Int32 methodId, global::System.Object taskCompletionSource, global::System.ArraySegment<global::System.Byte> data)
        {
            switch (methodId)
            {
            }
        }
        
    }
}

