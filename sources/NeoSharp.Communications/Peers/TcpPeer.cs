using System;
using System.Net.Sockets;
using System.Threading;
using NeoSharp.Logging;

namespace NeoSharp.Communications.Peers
{
    public class TcpPeer : IPeer
    {
        private readonly ILogger<TcpPeer> logger;
        private readonly ITcpStreamerFactory tcpStreamerFactory;

        private Socket peerSocket;
        private NetworkStream peerNetworkStream;
        private CancellationTokenSource cancellationTokenSource;

        public bool Connected { get; private set; }

        public bool IsReady { get; private set; }

        public TcpPeer(ITcpStreamerFactory tcpStreamerFactory, ILogger<TcpPeer> logger)
        {
            this.tcpStreamerFactory = tcpStreamerFactory;
            this.logger = logger;

            this.cancellationTokenSource = new CancellationTokenSource();
        }

        public async void Connect(PeerEndPoint peerEndPoint)
        {
            if (this.Connected)
            {
                this.logger.LogWarning($"Cannot connect to the peer {peerEndPoint.ToString()} because it's already connected.");
                return;
            }

            if (await this.tcpStreamerFactory.Connect(peerEndPoint))
            {
                this.Connected = true;

                this.SendMessageSmartQueueMonitorInitialization();
                this.PeerListenerInitialization();
            }
        }

        private void PeerListenerInitialization()
        {
        }

        private void SendMessageSmartQueueMonitorInitialization()
        {
            
        }
    }
}