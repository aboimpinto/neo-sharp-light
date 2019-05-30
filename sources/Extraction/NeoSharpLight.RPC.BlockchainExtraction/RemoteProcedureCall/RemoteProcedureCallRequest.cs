using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeoSharpLight.RPC.BlockchainExtraction.RemoteProcedureCall
{
    [Serializable]
    public class RemoteProcedureCallRequest
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpcVersion = "2.0";

        [JsonProperty("method")]
        public RemoteProcedureCallMethod Method = RemoteProcedureCallMethod.none;

        [JsonProperty("params")]
        public List<object> Parameters = new List<object>();

        [JsonProperty("id")]
        public int Id = 1;
    }
}