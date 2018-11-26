using System;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class Contract
    {
        public string Hash => Code.Hash;

        public string Script => Code.Script;

        public string[] Parameters => Code.Parameters;

        public string ReturnType => Code.ReturnType;

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("needstorage")]
        public string NeedStorage;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("version")]
        public string Version;

        [JsonProperty("author")]
        public string Author;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("description")]
        public string Description;
    }
}
