using System;
using System.Threading;
using System.Threading.Tasks;
using NeoSharp.Communications.Messages;
using NeoSharp.Communications.Protocols;
using NeoSharp.Core;
using NeoSharp.Logging;

namespace NeoSharp.Communications.Peers
{
    public class TcpPeer : IPeer, IDisposable
    {
        private const int SocketOperationTimeout = 300_000;

        private readonly ILogger<TcpPeer> logger;
        private readonly ICommunicationsContext communicationsContext;
        private readonly ITcpStreamerFactory tcpStreamerFactory;
        private readonly IProtocol protocol;
        private readonly SafeQueue<IMessage> sendMessageQueue;

        private readonly CancellationTokenSource cancellationTokenSource;
        private PeerEndPoint peerEndPoint;

        private bool disposed;

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
            this.sendMessageQueue = new SafeQueue<IMessage>();
        }

        public async void Connect(PeerEndPoint endPoint)
        {
            this.peerEndPoint = endPoint;

            if (this.Connected)
            {
                this.logger.LogWarning($"Cannot connect to the peer {peerEndPoint} because it's already connected.");
                return;
            }

            if (!await this.tcpStreamerFactory.Connect(peerEndPoint).ConfigureAwait(false)) return;

            this.Connected = true;

            this.SendMessageSmartQueueMonitorInitialization();
            this.PeerListenerInitialization();
        }

        public void Disconnect()
        {
            this.Dispose();
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
                this.cancellationTokenSource.Cancel();
                this.tcpStreamerFactory.Dispose();
                this.cancellationTokenSource.Dispose();

                this.logger.LogInformation($"The peer {this.peerEndPoint.ToString()} was disconnected.");
            }

            this.disposed = true;
        }

        private void PeerListenerInitialization()
        {
            this.QueueMessageToSend(this.communicationsContext.VersionMessage);

            Task.Run(async () =>
            {
                while (!this.cancellationTokenSource.IsCancellationRequested)
                {
                    if (this.tcpStreamerFactory.TcpNetworkStream.DataAvailable)
                    {
                        var receivedMessage = await this.ReceiveAsync().ConfigureAwait(false);
                    }
                }
            }, this.cancellationTokenSource.Token);
        }

        private void SendMessageSmartQueueMonitorInitialization()
        {
            Task.Run(async () => 
            {
                while (!this.cancellationTokenSource.IsCancellationRequested)
                {
                    this.sendMessageQueue.WaitForQueueToChange();

                    var message = this.sendMessageQueue.Dequeue();

                    if (message == null) continue;

                    if (message.Command != MessageCommand.consensus)
                    {
                        await this.SendMessageAsync(message).ConfigureAwait(false);
                    }
                }
            }, this.cancellationTokenSource.Token);
        }

        private async Task SendMessageAsync(IMessage message)
        {
            using(var socketTokenSource = new CancellationTokenSource(SocketOperationTimeout))
            {
                socketTokenSource.Token.Register(this.Disconnect);

                try
                {
                    this.logger.LogDebug($"Sending message {message.Command} send to {this.peerEndPoint}.");
                    if (this.tcpStreamerFactory.StillAlive)
                    {
                        await this.protocol.SendMessageAsync(this.tcpStreamerFactory.TcpNetworkStream, message, socketTokenSource.Token).ConfigureAwait(false);
                        this.logger.LogInformation($"Message {message.Command} sent to {this.peerEndPoint}.");
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, $"Error while sending message {message.Command} to {this.peerEndPoint}.");
                    this.Disconnect();
                }
            }
        }

        private async Task<Message> ReceiveAsync()
        {
            using (var receiveCancellationTokenSource = new CancellationTokenSource(SocketOperationTimeout))
            {
                receiveCancellationTokenSource.Token.Register(this.Disconnect);

                try
                {
                    var msg = await this.protocol.ReceiveMessageAsync(this.tcpStreamerFactory.TcpNetworkStream, receiveCancellationTokenSource.Token).ConfigureAwait(false);

                    if (msg == null)
                    {
                        this.logger.LogError("Unknown error receiving message");
                        return null;
                    }
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

        private void QueueMessageToSend(IMessage message)
        {
            this.sendMessageQueue.Enqueue(message);
        }
    }
}