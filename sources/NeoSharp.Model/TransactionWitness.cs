using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class TransactionWitness
    {
        public int Id { get; set; }

        [JsonProperty("invocation")]
        public string InvocationScript { get; set; }

        [JsonProperty("verification")]
        public string VerificationScript { get; set; }

        public string Hash { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}