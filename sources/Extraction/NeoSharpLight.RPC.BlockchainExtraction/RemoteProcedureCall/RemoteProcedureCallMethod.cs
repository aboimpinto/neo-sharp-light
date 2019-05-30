using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NeoSharpLight.RPC.BlockchainExtraction.RemoteProcedureCall
{
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RemoteProcedureCallMethod
    {
        none,

        getassetstate,

        getblockcount,

        getblock
    }
}