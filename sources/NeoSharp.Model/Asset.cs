using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    public class Asset
    {
        [JsonProperty("hash")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public AssetType AssetType { get; set; }

#if !DEBUG //TODO: Chinese characters are having an issue in the VS Debugger?  We can't serialize Chinese Chars?
        [BinaryProperty(3)]
        [JsonProperty("name")]
        public string Name { get; set; }
#endif

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("precision")]
        public byte Precision { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("admin")]
        public string Admin { get; set; }

        [JsonProperty("transactions")]
        public virtual IEnumerable<Transaction> Transactions { get; set; }

        public string TransactionHash { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}