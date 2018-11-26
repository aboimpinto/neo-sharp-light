using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class CoinReference
    {
        [JsonProperty("txid")]
        public string PrevHash;

        [JsonProperty("vout")]
        public int PrevIndex;

        [JsonProperty("id")]
        public string Id => $"{PrevHash}_{PrevIndex}";
    }
}