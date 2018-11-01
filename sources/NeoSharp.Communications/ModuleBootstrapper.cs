using System;
using NeoSharp.Communications.PeerFactory;
using NeoSharp.Communications.Peers;
using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharp.Communications
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.Register<IPeer, TcpPeer>(NodeProtocol.tcp.ToString());

            Func<NodeProtocol, IPeer> funcFactory = nodeProtocol => container.Resolve<IPeer>(nodeProtocol.ToString());
            var peerFactory = new PeerFactory.PeerFactory(funcFactory);
            container.Register<IPeerFactory>(peerFactory);

            container.Register<INodeConnector, NodeConnector>();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}