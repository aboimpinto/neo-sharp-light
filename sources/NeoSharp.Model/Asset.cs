using Newtonsoft.Json;

namespace NeoSharp.Model
{
    public class Asset
    {
        [JsonProperty("hash")]
        public string Id;

        [JsonProperty("type")]
        public AssetType AssetType;

#if !DEBUG //TODO: Chinese characters are having an issue in the VS Debugger?  We can't serialize Chinese Chars?
        [BinaryProperty(3)]
        [JsonProperty("name")]
        public string Name;
#endif

        [JsonProperty("amount")]
        public double Amount;

        [JsonProperty("precision")]
        public byte Precision;

        [JsonProperty("owner")]
        public string Owner;

        [JsonProperty("admin")]
        public string Admin;

        [JsonProperty("transactions")]
        public Transaction[] Transactions = new Transaction[0];
    }
}