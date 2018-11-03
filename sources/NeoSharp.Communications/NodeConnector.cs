using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeoSharp.Communications.PeerFactory;
using NeoSharp.Core;
using NeoSharp.Core.Extensions;
using NeoSharp.Logging;

namespace NeoSharp.Communications
{
    public class NodeConnector : INodeConnector
    {
        private readonly IPeerFactory peerFactory;
        private readonly ILogger<NodeConnector> logger;
        private bool isNodeRunning;
        private NetworkConfiguration networkConfiguration;
        private IList<IPeer> connectedPeers;

        public NodeConnector(
            INeoSharpContext neoSharpContext,
            IPeerFactory peerFactory,
            ILogger<NodeConnector> logger)
        {
            this.networkConfiguration = neoSharpContext.ApplicationConfiguration.LoadConfiguration<NetworkConfiguration>();
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
            Parallel.ForEach(this.networkConfiguration.Peers, peerAddress => 
            {
                var peerEndPoint = new PeerEndPoint(peerAddress);

                var peer = this.peerFactory.Create(peerEndPoint.NodeProtocol);
                peer.Connect(peerEndPoint);

                this.connectedPeers.Add(peer);
            });
        }
    }
}