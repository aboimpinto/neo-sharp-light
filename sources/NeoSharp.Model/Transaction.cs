using System;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class Transaction
    {
        [JsonProperty("txid")]
        public string Hash;

        [JsonProperty("size")]
        public int Size;

        [JsonProperty("type")]
        public TransactionType Type;

        [JsonProperty("version")]
        public byte Version;

        [JsonProperty("sys_fee")]
        public long SystemFee;

        [JsonProperty("net_fee")]
        public long NetworkFee;

        [JsonProperty("blockhash")]
        public string BlockHash;

        [JsonProperty("blockindex")]
        public int BlockIndex;

        [JsonProperty("timestamp")]
        public int Timestamp;

        [JsonProperty("attributes")]
        public TransactionAttribute[] Attributes = new TransactionAttribute[0];

        [JsonProperty("vin")]
        public CoinReference[] Inputs = new CoinReference[0];

        [JsonProperty("vout")]
        public TransactionOutput[] Outputs = new TransactionOutput[0];

        [JsonProperty("scripts")]
        public Witness[] Scripts;

        //Enrollment
        [JsonProperty("pubkey")]
        public string PublicKey;

        //Invocation
        [JsonProperty("script")]
        public string Script;

        //Invocation
        [JsonProperty("gas")]
        public long Gas;

        //Miner
        [JsonProperty("nonce")]
        public ulong Nonce;

        //Claim
        [JsonProperty("claims")]
        public CoinReference[] Claims = new CoinReference[0];

        //Publish
        [JsonProperty("contract")]
        public Contract Contract;

        //Register
        [JsonProperty("asset")]
        public Asset Asset;
    }
}
