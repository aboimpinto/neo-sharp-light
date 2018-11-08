using System.Threading;

namespace NeoSharp.Communications
{
    public interface IPeer
    {
        bool Connected { get; }

         bool IsReady { get; }

        void Connect(PeerEndPoint peerEndPoint);
    }
}