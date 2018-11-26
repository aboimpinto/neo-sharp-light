using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class Code
    {
        [JsonProperty("hash")]
        public string Hash;

        [JsonProperty("script")]
        public string Script;

        [JsonProperty("returntype")]
        public string ReturnType;

        [JsonProperty("parameters")]
        public string[] Parameters = new string[0];
    }
}