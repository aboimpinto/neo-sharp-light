using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class Code
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("returntype")]
        public string ReturnType { get; set; }

        [JsonProperty("parameters")]
        public string[] Parameters = new string[0];

        public virtual Contract Contract { get; set; }
    }
}