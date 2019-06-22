namespace NeoSharpLight.RPC.BlockchainExtraction.RpcCall
{
    public class RpcCallRequestBuilder
    {
        private RpcCallMethod _method;

        public RpcCallRequestBuilder WithMethod(RpcCallMethod method)
        {
            this._method = method;
            return this;
        }

        public RpcCallRequest Build()
        {
            return new RpcCallRequest
            {
                Method = this._method
            };
        }
    }
}