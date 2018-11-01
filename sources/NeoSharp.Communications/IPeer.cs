namespace NeoSharp.Communications
{
    public interface IPeer
    {
         bool IsReady { get; }

        void Connect(PeerEndPoint peerEndPoint);
    }
}