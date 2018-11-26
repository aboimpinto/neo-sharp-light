using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class TransactionAttribute
    {
        [JsonProperty("usage")]
        public TransactionAttributeUsage Usage;

        [JsonProperty("data")]
        public string Data;
    }
}
