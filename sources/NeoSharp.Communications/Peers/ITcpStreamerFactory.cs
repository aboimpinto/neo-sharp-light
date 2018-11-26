using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NeoSharp.Communications.Peers
{
    public interface ITcpStreamerFactory : IDisposable
    {
        bool StillAlive { get; }

        NetworkStream TcpNetworkStream { get; }

         Task<bool> Connect(PeerEndPoint endPoint);
    }
}