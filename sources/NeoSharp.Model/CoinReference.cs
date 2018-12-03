using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class CoinReference
    {
        private string id;

        [JsonProperty("txid")]
        public string PrevHash { get; set; }

        [JsonProperty("vout")]
        public int PrevIndex { get; set; }

        [JsonProperty("id")]
        public string Id
        {
            get { return this.id;}
            set { this.id = value; }
        }

        public string TransactionHash { get; set; }

        public virtual Transaction Transaction { get; set; }

        public CoinReference()
        {
            this.id = $"{PrevHash}_{PrevIndex}";
        }
    }
}