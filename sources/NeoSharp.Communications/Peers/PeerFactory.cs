using System;

namespace NeoSharp.Communications.Peers
{
    public class PeerFactory : IPeerFactory
    {
        private readonly Func<NodeProtocol, IPeer> funcFactory;

        public PeerFactory(Func<NodeProtocol, IPeer> funcFactory)
        {
            this.funcFactory = funcFactory;
        }

        public IPeer Create(NodeProtocol nodeProtocol)
        {
            return this.funcFactory(nodeProtocol);
        }
    }
}