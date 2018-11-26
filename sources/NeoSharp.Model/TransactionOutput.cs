using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class TransactionOutput
    {
        [JsonProperty("n")]
        public string Index;

        [JsonProperty("asset")]
        public string AssetId;

        [JsonProperty("value")]
        public double Value;

        [JsonProperty("address")]
        public string Address;
    }
}