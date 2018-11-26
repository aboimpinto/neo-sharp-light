using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class Witness
    {
        [JsonProperty("invocation")]
        public string InvocationScript;

        [JsonProperty("verification")]
        public string VerificationScript;
    }
}