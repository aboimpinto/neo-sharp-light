using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class Contract
    {
        public string Hash
        {
            get => Code.Hash;
            set => Code.Hash = value;
        }

        public string Script => Code.Script;

        public IEnumerable<string> Parameters => Code.Parameters;

        public string ReturnType => Code.ReturnType;

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("needstorage")]
        public string NeedStorage { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
