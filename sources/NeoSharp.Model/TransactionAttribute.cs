using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class TransactionAttribute
    {
        public int Id { get; set; }

        public string TransactionHash { get; set; }

        [JsonProperty("usage")]
        public TransactionAttributeUsage Usage { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
