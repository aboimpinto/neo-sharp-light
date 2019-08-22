namespace NeoSharpLight.RPC.BlockchainExtraction.Node
{
    public interface INodeManager
    {
        int GetBlockCount();

        dynamic GetRawBlock(int index);
    }
}