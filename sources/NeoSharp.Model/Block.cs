using System;
using System.Linq;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class Block
    {
        [JsonProperty("hash")]
        public string Hash;

        [JsonProperty("size")]
        public int Size;

        [JsonProperty("version")]
        public byte Version;

        [JsonProperty("previousblockhash")]
        public string PreviousBlockHash;

        [JsonProperty("merkleroot")]
        public string MerkleRoot;

        [JsonProperty("time")]
        public int Timestamp;

        [JsonProperty("index")]
        public int Index;

        [JsonProperty("nonce")]
        public string ConsensusData;

        [JsonProperty("nextconsensus")]
        public string NextConsensus;

        [JsonProperty("nextblockhash")]
        public string NextBlockHash;

        [JsonProperty("tx")]
        public Transaction[] Transactions = new Transaction[0];

        [JsonProperty("script")]
        public Witness Script;

        [JsonProperty("confirmations")]
        public int Confirmations;

        [JsonProperty("txcount")]
        public int TxCount => Transactions.Length;

        [JsonProperty("txhashes")]
        public string[] TxHashes
        {
            get
            {
                return Transactions != null ? 
                    Transactions.Select(h => h.BlockHash).ToArray() : 
                    new string[0];
            }
        }
    }
}
