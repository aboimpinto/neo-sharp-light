namespace NeoSharpLight.RPC.BlockchainExtraction.RemoteProcedureCall
{
    public interface IRemoteProcedureCallManager
    {
        dynamic RpcCall(RemoteProcedureCallRequest request);
    }
}