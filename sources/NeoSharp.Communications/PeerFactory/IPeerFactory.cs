namespace NeoSharp.Communications.PeerFactory
{
    public interface IPeerFactory
    {
         IPeer Create(NodeProtocol nodeProtocol);
    }
}