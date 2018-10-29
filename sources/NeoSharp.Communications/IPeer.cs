namespace NeoSharp.Communications
{
    public interface IPeer
    {
         bool IsReady { get; }

         bool CanHandle(NodeProtocol nodeProtocol);

        void Connect(PeerEndPoint peerEndPoint);
    }
}