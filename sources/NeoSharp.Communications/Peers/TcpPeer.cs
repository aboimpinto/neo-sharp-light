namespace NeoSharp.Communications.Peers
{
    public class TcpPeer : IPeer
    {
        public bool IsReady { get; private set; }

        public TcpPeer()
        {
        }

        public void Connect(PeerEndPoint peerEndPoint)
        {
            throw new System.NotImplementedException();
        }
    }
}