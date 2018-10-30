using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeoSharp.Core;
using NeoSharp.Core.Extensions;
using NeoSharp.Logging;

namespace NeoSharp.Communications
{
    public class NodeConnector : INodeConnector
    {
        private readonly ILogger<NodeConnector> logger;
        private bool isNodeRunning;
        private NetworkConfiguration networkConfiguration;
        private IEnumerable<PeerEndPoint> peersEndPoint;

        public NodeConnector(
            INeoSharpContext neoSharpContext,
            ILogger<NodeConnector> logger)
        {
            this.networkConfiguration = neoSharpContext.ApplicationConfiguration.LoadConfiguration<NetworkConfiguration>();
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

            // Parallel.ForEach(this.networkConfiguration.Peers, peer => 
            // {

            // });
        }
    }
}