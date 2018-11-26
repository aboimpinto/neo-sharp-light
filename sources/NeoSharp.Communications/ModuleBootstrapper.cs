using System.Linq;
using NeoSharp.Communications.Messages;
using NeoSharp.Communications.Peers;
using NeoSharp.Communications.Protocols;
using NeoSharp.Core;
using NeoSharp.DependencyInjection;
using NeoSharp.Serialization.CustomSerializers;

namespace NeoSharp.Communications
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.RegisterSingleton<ICommunicationsContext, CommunicationsContext>();

            container.Register<IPeer, TcpPeer>(NodeProtocol.tcp.ToString());
            container.Register<IPeer, HttpPeer>(NodeProtocol.http.ToString());

            IPeer funcPeerFactory(NodeProtocol nodeProtocol) => container.Resolve<IPeer>(nodeProtocol.ToString());
            var peerFactory = new PeerFactory(funcPeerFactory);
            container.Register<IPeerFactory>(peerFactory);

            container.Register<ITcpStreamerFactory, TcpStreamerFactory>();
            container.Register<INodeConnector, NodeConnector>();

            container.Register<IMessage, VersionMessage>(MessageCommand.version.ToString());

            var typesOfMessages = typeof(IMessage)
                .Assembly
                .GetTypes()
                .Where(x => x.IsClass)
                .Where(x => typeof(IMessage).IsAssignableFrom(x))
                .Select(x => x.UnderlyingSystemType)
                .ToList();
            container.RegisterTypes(typesOfMessages);

            IMessage funcMessageFactory(MessageCommand messageCommand) => container.Resolve<IMessage>(messageCommand.ToString());
            var messageFactory = new MessageFactory(funcMessageFactory);
            container.Register<IMessageFactory>(messageFactory);

            container.Register<IProtocol, ProtocolV1>();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}