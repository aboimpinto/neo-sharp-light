using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class Transaction
    {
        [JsonProperty("txid")]
        public string Hash { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("type")]
        public TransactionType Type { get; set; }

        [JsonProperty("version")]
        public byte Version { get; set; }

        [JsonProperty("sys_fee")]
        public long SystemFee { get; set; }

        [JsonProperty("net_fee")]
        public long NetworkFee { get; set; }

        [JsonProperty("blockhash")]
        public string BlockHash { get; set; }

        [JsonProperty("blockindex")]
        public int BlockIndex { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        //Enrollment
        [JsonProperty("pubkey")]
        public string PublicKey { get; set; }

        //Invocation
        [JsonProperty("script")]
        public string Script { get; set; }

        //Invocation
        [JsonProperty("gas")]
        public long Gas { get; set; }

        //Miner
        [JsonProperty("nonce")]
        public long Nonce { get; set; }

        //Publish
        [JsonProperty("contract")]
        public Contract Contract { get; set; }

        //Register
        [JsonProperty("asset")]
        public Asset Asset { get; set; }

        [JsonProperty("vout")]
        public virtual IEnumerable<TransactionOutput> Outputs { get; set; }

        [JsonProperty("attributes")]
        public virtual IEnumerable<TransactionAttribute> Attributes { get; set; }

        [JsonProperty("vin")]
        public virtual IEnumerable<CoinReference> Inputs { get; set; }

        //Claim
        [JsonProperty("claims")]
        public virtual IEnumerable<CoinReference> Claims { get; set; }

        [JsonProperty("scripts")]
        public virtual IEnumerable<TransactionWitness> Scripts { get; set; }

        public virtual Block Block { get; set; }
    }
}
