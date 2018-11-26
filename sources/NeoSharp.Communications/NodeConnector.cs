using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NeoSharp.Communications.Peers;
using NeoSharp.Logging;

namespace NeoSharp.Communications
{
    public class NodeConnector : INodeConnector
    {
        private readonly ICommunicationsContext communicationsContext;
        private readonly IPeerFactory peerFactory;
        private readonly ILogger<NodeConnector> logger;
        private bool isNodeRunning;
        private IList<IPeer> connectedPeers;

        public NodeConnector(
            ICommunicationsContext communicationsContext,
            IPeerFactory peerFactory,
            ILogger<NodeConnector> logger)
        {
            this.communicationsContext = communicationsContext;
            this.peerFactory = peerFactory;
            this.logger = logger;

            this.connectedPeers = new List<IPeer>();
        }

        /// <inheritdoc />
        public void Connect()
        {
            this.logger.LogInformation("Begin NodeConnector.Connect");

            if (this.isNodeRunning)
            {
                throw new InvalidOperationException("Node is already running.");
            }

            this.ConnectToPeers();

            this.isNodeRunning = true;
        }

        private void ConnectToPeers()
        {
            Parallel.ForEach(this.communicationsContext.NetworkConfiguration.Peers, peerAddress => 
            {
                var peerEndPoint = new PeerEndPoint(peerAddress);

                var peer = this.peerFactory.Create(peerEndPoint.NodeProtocol);
                peer.Connect(peerEndPoint);

                this.connectedPeers.Add(peer);
            });
        }
    }
}