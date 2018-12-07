namespace NeoSharpLight.RPC.NodeManager
{
    public interface INodeAccess
    {
        int GetBlockCount();

        string GetBlock(int index);
    }
}
