using System.Net;

namespace NeoSharp.Communications
{
    public interface IPeerEndPoint
    {
        NodeProtocol NodeProtocol { get; }

        string Host { get; }

        IPAddress IpAddress { get; }

        int Port { get; }
    }
}