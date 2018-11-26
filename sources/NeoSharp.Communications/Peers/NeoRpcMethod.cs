using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NeoSharp.Communications.Peers
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