using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NeoSharp.Communications.Peers
{
    public interface ITcpStreamerFactory
    {
        NetworkStream TcpNetworkStream { get; }

         Task<bool> Connect(PeerEndPoint peerEndPoint);
    }
}