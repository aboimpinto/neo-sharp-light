namespace NeoSharp.Communications.Peers
{
    public interface IPeerFactory
    {
         IPeer Create(NodeProtocol nodeProtocol);
    }
}