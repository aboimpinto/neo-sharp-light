using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NeoSharpLight.RPC.NodeManager.RpcNodeAccess
{
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NeoRpcMethod
    {
        none,
        getassetstate,
        getblockcount,
        getblock
    }
}
