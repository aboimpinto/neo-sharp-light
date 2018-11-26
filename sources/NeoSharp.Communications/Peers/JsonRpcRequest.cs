using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeoSharp.Communications.Peers
{
    [Serializable]
    public class JsonRpcRequest
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpcVersion = "2.0";

        [JsonProperty("method")]
        public NeoRpcMethod Method = NeoRpcMethod.none;

        [JsonProperty("params")]
        public List<object> Parameters = new List<object>();

        [JsonProperty("id")]
        public int Id = 1;
    }
}
