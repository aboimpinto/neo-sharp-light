using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using NeoSharp.Logging;

namespace NeoSharp.Communications.Peers
{
    public class TcpStreamerFactory : ITcpStreamerFactory
    {
        private readonly ILogger<TcpStreamerFactory> logger;

        private bool disposed;
        private PeerEndPoint peerEndPoint;
        private Socket socket;

        public NetworkStream TcpNetworkStream { get; private set; }

        public bool StillAlive
        {
            get
            {
                if (!this.socket.Poll(0, SelectMode.SelectRead)) return true;

                var buffer = new byte[1];
                return this.socket.Receive(buffer, SocketFlags.Peek) != 0;
            }
        }

        public TcpStreamerFactory(ILogger<TcpStreamerFactory> logger)
        {
            this.logger = logger;
        }

        public async Task<bool> Connect(PeerEndPoint endPoint)
        {
            this.peerEndPoint = endPoint;

            this.logger.LogDebug($"Connecting to {endPoint}...");
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                await socket.ConnectAsync(endPoint.IpAddress, endPoint.Port).ConfigureAwait(false);
                this.logger.LogInformation($"Connection to {endPoint} established.");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Connection to {endPoint} failed.");
                return false;
            }

            this.TcpNetworkStream = new NetworkStream(socket, true);

            return true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                //this.peerSocket.Shutdown(SocketShutdown.Both);
                this.TcpNetworkStream.Dispose();
                //this.peerSocket.Dispose();

                this.logger.LogInformation($"The peer {this.peerEndPoint.ToString()} was disconnected.");
            }

            this.disposed = true;
        }
    }
}