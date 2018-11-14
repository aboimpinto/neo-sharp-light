using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using NeoSharp.Logging;

namespace NeoSharp.Communications.Peers
{
    public class TcpStreamerFactory : ITcpStreamerFactory
    {
        private readonly ILogger<TcpStreamerFactory> logger;

        public NetworkStream TcpNetworkStream { get; private set; }

        public TcpStreamerFactory(ILogger<TcpStreamerFactory> logger)
        {
            this.logger = logger;
        }

        public async Task<bool> Connect(PeerEndPoint peerEndPoint)
        {
            this.logger.LogDebug($"Connecting to {peerEndPoint.ToString()}...");
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                await socket.ConnectAsync(peerEndPoint.IpAddress, peerEndPoint.Port).ConfigureAwait(false);
                this.logger.LogError($"Connection to {peerEndPoint.ToString()} established.");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Connection to {peerEndPoint.ToString()} failed.");
                return false;
            }

            this.TcpNetworkStream = new NetworkStream(socket, true);

            return true;
        }
    }
}