using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using NeoSharp.Communications.Messages;
using NeoSharp.Communications.Protocols;
using NeoSharp.Core;
using NeoSharp.Core.Extensions;
using NeoSharp.Logging;

namespace NeoSharp.Communications.Peers
{
    public class TcpPeer : IPeer
    {
        private const int SocketOperationTimeout = 300_000;

        private readonly ILogger<TcpPeer> logger;
        private readonly ICommunicationsContext communicationsContext;
        private readonly ITcpStreamerFactory tcpStreamerFactory;
        private readonly IProtocol protocol;
        private readonly SafeQueue<Message> sendMessageQueue;

        private Socket peerSocket;
        private NetworkStream peerNetworkStream;
        private CancellationTokenSource cancellationTokenSource;
        private PeerEndPoint peerEndPoint;

        public bool Connected { get; private set; }

        public bool IsReady { get; private set; }

        public TcpPeer(
            ITcpStreamerFactory tcpStreamerFactory,
            IProtocol protocol,
            ILogger<TcpPeer> logger, 
            ICommunicationsContext communicationsContext)
        {
            this.tcpStreamerFactory = tcpStreamerFactory;
            this.protocol = protocol;
            this.logger = logger;
            this.communicationsContext = communicationsContext;
            this.cancellationTokenSource = new CancellationTokenSource();
            this.sendMessageQueue = new SafeQueue<Message>();
        }

        public async void Connect(PeerEndPoint peerEndPoint)
        {
            this.peerEndPoint = peerEndPoint;

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

        public void Disconnect()
        {
            this.cancellationTokenSource.Cancel();

            this.peerSocket.Shutdown(SocketShutdown.Both);
            this.peerNetworkStream.Dispose();
            this.peerSocket.Dispose();

            this.logger.LogInformation($"The peer {this.peerEndPoint.ToString()} was disconnected.");
        }

        public void QueueMessageToSend(Message message)
        {
            this.sendMessageQueue.Enqueue(message);
        }

        private void PeerListenerInitialization()
        {
            this.QueueMessageToSend(new VersionMessage(this.communicationsContext.VersionPayload));

            Task.Factory.StartNew(async () =>
            {
                while (!this.cancellationTokenSource.IsCancellationRequested)
                {
                    if (this.peerNetworkStream.DataAvailable)
                    {
                        var receivedMessage = await this.Receive();
                    }
                }
            });
        }

        private void SendMessageSmartQueueMonitorInitialization()
        {
            Task.Factory.StartNew(async () =>
            {
                while(!this.cancellationTokenSource.IsCancellationRequested)
                {
                    this.sendMessageQueue.WaitForQueueToChange();

                    var message = this.sendMessageQueue.Dequeue();

                    if (message == null) continue;

                    if (message.Command != MessageCommand.consensus)
                    {
                        await this.SendMessage(message);
                    }
                }
            });
        }

        private async Task SendMessage(Message message)
        {
            using(var socketTokenSource = new CancellationTokenSource(SocketOperationTimeout))
            {
                socketTokenSource.Token.Register(this.Disconnect);

                try
                {
                    this.logger.LogDebug($"Sending message {message.Command} send to {this.peerEndPoint.ToString()}.");
                    await this.protocol.SendMessageAsync(this.peerNetworkStream, message, socketTokenSource.Token);
                    this.logger.LogInformation($"Message {message.Command} sended to {this.peerEndPoint.ToString()}.");
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, $"Error while sending message {message.Command} to {this.peerEndPoint.ToString()}.");
                    this.Disconnect();
                }
            }
        }

        public async Task<Message> Receive()
        {
            using (var receiveCancelationTokenSource = new CancellationTokenSource(SocketOperationTimeout))
            {
                receiveCancelationTokenSource.Token.Register(this.Disconnect);

                try
                {
                    var msg = await this.protocol.ReceiveMessageAsync(this.peerNetworkStream, receiveCancelationTokenSource.Token);
                    this.logger.LogInformation($"Message received: {msg.Command}.");

                    return msg;
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "Error receiving a message.");
                    this.Disconnect();
                }
            }

            return null;
        }
    }
}