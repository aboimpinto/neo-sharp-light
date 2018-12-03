using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace NeoSharp.Model
{
    [Serializable]
    public class Block
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("version")]
        public byte Version { get; set; }

        [JsonProperty("previousblockhash")]
        public string PreviousBlockHash { get; set; }

        [JsonProperty("merkleroot")]
        public string MerkleRoot { get; set; }

        [JsonProperty("time")]
        public int Timestamp { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("nonce")]
        public string ConsensusData { get; set; }

        [JsonProperty("nextconsensus")]
        public string NextConsensus { get; set; }

        [JsonProperty("nextblockhash")]
        public string NextBlockHash { get; set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }

        [JsonProperty("script")]
        public virtual BlockWitness Script { get; set; }

        [NotMapped]
        [JsonProperty("tx")]
        public virtual ICollection<Transaction> Transactions { get; set; }

        [NotMapped]
        [JsonProperty("txcount")]
        public int TxCount => Transactions.Count;

        [NotMapped]
        [JsonProperty("txhashes")]
        public IEnumerable<string> TxHashes
        {
            get
            {
                return Transactions != null ? 
                    Transactions.Select(h => h.BlockHash).ToList() : 
                    new List<string>();
            }
        }
    }
}
