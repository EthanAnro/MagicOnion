﻿// <auto-generated />
#pragma warning disable CS0618 // 'member' is obsolete: 'text'
#pragma warning disable CS0612 // 'member' is obsolete


namespace TempProject
{
    using global::System;
    using global::Grpc.Core;
    using global::MagicOnion;
    using global::MagicOnion.Client;
    using global::MessagePack;
    
    [global::MagicOnion.Ignore]
    public class MyServiceClient : global::MagicOnion.Client.MagicOnionClientBase<global::TempProject.IMyService>, global::TempProject.IMyService
    {
        class ClientCore
        {
            public global::MagicOnion.Client.Internal.RawMethodInvoker<global::System.Collections.Generic.List<global::TempProject.MyResponse>, global::MessagePack.Nil> GetValuesAsync;
            public ClientCore(global::MagicOnion.Serialization.IMagicOnionSerializerProvider serializerProvider)
            {
                this.GetValuesAsync = global::MagicOnion.Client.Internal.RawMethodInvoker.Create_RefType_ValueType<global::System.Collections.Generic.List<global::TempProject.MyResponse>, global::MessagePack.Nil>(global::Grpc.Core.MethodType.Unary, "IMyService", "GetValuesAsync", serializerProvider);
            }
        }
        
        readonly ClientCore core;
        
        public MyServiceClient(global::MagicOnion.Client.MagicOnionClientOptions options, global::MagicOnion.Serialization.IMagicOnionSerializerProvider serializerProvider) : base(options)
        {
            this.core = new ClientCore(serializerProvider);
        }
        
        private MyServiceClient(MagicOnionClientOptions options, ClientCore core) : base(options)
        {
            this.core = core;
        }
        
        protected override global::MagicOnion.Client.MagicOnionClientBase<IMyService> Clone(global::MagicOnion.Client.MagicOnionClientOptions options)
            => new MyServiceClient(options, core);
        
        public global::MagicOnion.UnaryResult<global::MessagePack.Nil> GetValuesAsync(global::System.Collections.Generic.List<global::TempProject.MyResponse> arg0)
            => this.core.GetValuesAsync.InvokeUnary(this, "IMyService/GetValuesAsync", arg0);
    }
}

