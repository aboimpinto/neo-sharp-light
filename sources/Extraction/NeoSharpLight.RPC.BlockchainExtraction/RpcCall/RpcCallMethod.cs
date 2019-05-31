using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NeoSharpLight.RPC.BlockchainExtraction.RpcCall
{
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RpcCallMethod
    {
        none,

        getassetstate,

        getblockcount,

        getblock
    }
}