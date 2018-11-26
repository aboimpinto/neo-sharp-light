using System.Threading;
using NeoSharp.Communications.Messages;

namespace NeoSharp.Communications
{
    public interface IPeer
    {
        bool Connected { get; }

         bool IsReady { get; }

        void Connect(PeerEndPoint peerEndPoint);

        void Disconnect();

        //void QueueMessageToSend(IMessage message);
    }
}