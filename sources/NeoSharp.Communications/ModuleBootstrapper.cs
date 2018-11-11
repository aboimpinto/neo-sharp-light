using System;
using NeoSharp.Communications.PeerFactory;
using NeoSharp.Communications.Peers;
using NeoSharp.Communications.Protocols;
using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharp.Communications
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.RegisterSingleton<ICommunicationsContext, CommunicationsContext>();

            container.Register<IPeer, TcpPeer>(NodeProtocol.tcp.ToString());

            Func<NodeProtocol, IPeer> funcFactory = nodeProtocol => container.Resolve<IPeer>(nodeProtocol.ToString());
            var peerFactory = new PeerFactory.PeerFactory(funcFactory);
            container.Register<IPeerFactory>(peerFactory);

            container.Register<ITcpStreamerFactory, TcpStreamerFactory>();
            container.Register<INodeConnector, NodeConnector>();

            container.Register<IProtocol, ProtocolV1>();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}