using System.Collections.Generic;

namespace NeoSharpLight.RPC.BlockchainExtraction.RpcCall
{
    public class RpcCallRequestBuilder
    {
        private RpcCallMethod _method;
        private List<object> _parameters;

        public RpcCallRequestBuilder()
        {
            this._parameters = new List<object>();
        }

        public RpcCallRequestBuilder WithMethod(RpcCallMethod method)
        {
            this._method = method;
            return this;
        }

        public RpcCallRequestBuilder WithParameter(object parameter)
        {
            this._parameters.Add(parameter);
            return this;
        }

        public RpcCallRequest Build()
        {
            return new RpcCallRequest
            {
                Method = this._method,
                Parameters = this._parameters
            };
        }
    }
}