using System;
using NeoSharpLight.RPC.BlockchainExtraction.RpcCall;

namespace NeoSharpLight.RPC.BlockchainExtraction.Node
{
    public class NodeManager : INodeManager
    {
        private readonly IRpcCallManager _rpcCallManager;

        public NodeManager(IRpcCallManager rpcCallManager)
        {
            this._rpcCallManager = rpcCallManager;
        }

        public int GetBlockCount()
        {
            var request = new RpcCallRequestBuilder()
                .WithMethod(RpcCallMethod.getblockcount)
                .Build();

            var result = this._rpcCallManager.RpcCall(request);
            return result == null ? 0 : (int)int.Parse(result.ToString());
        }
    }
}