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
        private IEnumerable<PeerEndPoint> peersEndPoint;

        public NodeConnector(
            INeoSharpContext neoSharpContext,
            IPeerFactory peerFactory,
            ILogger<NodeConnector> logger)
        {
            this.networkConfiguration = neoSharpContext.ApplicationConfiguration.LoadConfiguration<NetworkConfiguration>();
            this.peerFactory = peerFactory;
            this.logger = logger;
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
            this.peersEndPoint = this.networkConfiguration
                .Peers
                .Select(x => new PeerEndPoint(x))
                .ToList();

            var peer1 = this.peerFactory.Create(this.peersEndPoint.First().NodeProtocol);
            var peer2 = this.peerFactory.Create(this.peersEndPoint.Last().NodeProtocol);

            // Parallel.ForEach(this.networkConfiguration.Peers, peer => 
            // {

            // });
        }
    }
}