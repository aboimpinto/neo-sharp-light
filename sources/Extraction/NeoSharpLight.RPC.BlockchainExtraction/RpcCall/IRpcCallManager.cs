namespace NeoSharpLight.RPC.BlockchainExtraction.RpcCall
{
    public interface IRpcCallManager
    {
        dynamic RpcCall(RpcCallRequest request);
    }
}