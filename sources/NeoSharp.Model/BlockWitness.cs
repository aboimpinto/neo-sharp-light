using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class BlockWitness
    {
        public int Id { get; set; }

        [JsonProperty("invocation")]
        public string InvocationScript { get; set; }

        [JsonProperty("verification")]
        public string VerificationScript { get; set; }

        public string Hash { get; set; }

        public virtual Block Block { get; set; }
    }
}