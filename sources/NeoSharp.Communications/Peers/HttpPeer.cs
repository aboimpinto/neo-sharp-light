using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore;
using NeoSharp.Logging;
using NeoSharp.Model;
using Newtonsoft.Json;

namespace NeoSharp.Communications.Peers
{
    public class HttpPeer : IPeer
    {
        private string rpcUrl;
        private int peerBlockCount;
        private int lastBlockExtracted;
        private readonly DbContext dbContext;
        private readonly ILogger<HttpPeer> logger;

        public bool Connected { get; }

        public bool IsReady { get; }

        public HttpPeer(
            DbContext dbContext,
            ILogger<HttpPeer> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;

            this.lastBlockExtracted = 0;
        }

        public void Connect(PeerEndPoint peerEndPoint)
        {
            this.rpcUrl = peerEndPoint.ToString();

            this.peerBlockCount = this.GetBlockCount();

            if (this.peerBlockCount == this.lastBlockExtracted)
            {
                return;
            }

            this.lastBlockExtracted = 4130;

            for (var i = this.lastBlockExtracted; i < this.peerBlockCount - 1; i++)
            {
                var block = this.RetrieveBlock(i);

                this.logger.LogInformation($"Block {block.Hash} with index {block.Index} retrieved.");

                this.dbContext.Add(block);
                this.dbContext.SaveChanges();

                this.lastBlockExtracted++;
            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        private int GetBlockCount()
        {
            var callResult = this.RpcCall(new JsonRpcRequest
            {
                Method = NeoRpcMethod.getblockcount
            });

            return callResult == null ? 
                0 : 
                (int) int.Parse(callResult.ToString());
        }

        private Block RetrieveBlock(int blockIndex)
        {
            var rawBlock = this.GetRawBlock(blockIndex);
            var block = JsonConvert.DeserializeObject<Block>(rawBlock);

            var transactionOutputId = 1;

            foreach (var blockTransaction in block.Transactions)
            {
                blockTransaction.BlockHash = block.Hash;

                if (blockTransaction.Asset != null)
                {
                    blockTransaction.Asset.TransactionHash = blockTransaction.Hash;
                    blockTransaction.Asset.Transaction = blockTransaction;
                }

                foreach (var transactionAttribute in blockTransaction.Attributes)
                {
                    transactionAttribute.Transaction = blockTransaction;
                    transactionAttribute.TransactionHash = blockTransaction.Hash;
                }

                foreach (var blockTransactionOutput in blockTransaction.Outputs)
                {
                    blockTransactionOutput.Transaction = blockTransaction;
                    blockTransactionOutput.TransactionHash = blockTransaction.Hash;
                    blockTransactionOutput.Id = transactionOutputId;

                    transactionOutputId++;
                }

                foreach (var transactionInput in blockTransaction.Inputs)
                {
                    transactionInput.Transaction = blockTransaction;
                    transactionInput.TransactionHash = blockTransaction.Hash;
                }

                if (blockTransaction.Claims != null)
                {
                    foreach (var transactionClaim in blockTransaction.Claims)
                    {
                        transactionClaim.Transaction = blockTransaction;
                        transactionClaim.TransactionHash = blockTransaction.Hash;
                    }
                }

                foreach (var transactionWitness in blockTransaction.Scripts)
                {
                    transactionWitness.Transaction = blockTransaction;
                    transactionWitness.Hash = blockTransaction.Hash;
                }

                if (blockTransaction.Contract != null)
                {
                    blockTransaction.Contract.Transaction = blockTransaction;
                    blockTransaction.Contract.Hash = blockTransaction.Hash;
                }
            }

            return block;
        }

        private string GetRawBlock(int blockIndex)
        {
            var callResult = this.RpcCall(new JsonRpcRequest
            {
                Method = NeoRpcMethod.getblock,
                Parameters = new List<object> { blockIndex, 1 }
            });

            return callResult?.ToString();
        }

        private dynamic RpcCall(JsonRpcRequest request)
        {
            using (var client = new WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var serializedRequest = JsonConvert.SerializeObject(request);
                var result = client.UploadString(this.rpcUrl, serializedRequest);

                var json = (dynamic)JsonConvert.DeserializeObject(result);
                if (json != null)
                {
                    if (json.error != null)
                    {
                        throw new Exception(json.message);
                    }
                    else if (json.result != null)
                    {
                        return json.result;
                    }
                }
            }

            return null;
        }
    }
}
