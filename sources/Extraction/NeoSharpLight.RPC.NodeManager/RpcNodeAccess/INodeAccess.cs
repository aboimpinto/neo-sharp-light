namespace NeoSharpLight.RPC.NodeManager.RpcNodeAccess
{
    public interface INodeAccess
    {
        int GetBlockCount();

        dynamic GetRawBlock(int index);
    }
}
