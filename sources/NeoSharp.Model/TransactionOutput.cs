using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class TransactionOutput
    {
        public int Id { get; set; }

        [JsonProperty("n")]
        public string Index { get; set; }

        [JsonProperty("asset")]
        public string AssetId { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        public string TransactionHash { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}