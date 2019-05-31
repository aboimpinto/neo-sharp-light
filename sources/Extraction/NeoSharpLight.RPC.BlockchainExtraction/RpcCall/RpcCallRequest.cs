using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeoSharpLight.RPC.BlockchainExtraction.RpcCall
{
    [Serializable]
    public class RpcCallRequest
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpcVersion = "2.0";

        [JsonProperty("method")]
        public RpcCallMethod Method = RpcCallMethod.none;

        [JsonProperty("params")]
        public List<object> Parameters = new List<object>();

        [JsonProperty("id")]
        public int Id = 1;
    }
}