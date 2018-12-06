using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NeoSharp.Core;
using NeoSharp.DbAccess;
using NeoSharp.DbAccess.MySQL;
using NeoSharp.Logging;
using NeoSharp.Model;
using Newtonsoft.Json;

namespace NeoSharp.Communications.Peers
{
    public class HttpPeer : IPeer
    {
        private string httpUrl;
        private int peerBlockCount;
        private readonly ICommunicationsContext communicationsContext;
        private readonly DbContext dbContext;
        private readonly ILogger<HttpPeer> logger;

        private readonly SafeQueue<BlockRaw> safeBlockRawQueue = new SafeQueue<BlockRaw>();

        //private int transactionOutputId;
        private bool retrievingBlocks = true;

        public bool Connected { get; }

        public bool IsReady { get; }

        public HttpPeer(
            ICommunicationsContext communicationsContext,
            BlockchainDbAccess blockchainDbAccess,
            DbContext dbContext,
            ILogger<HttpPeer> logger)
        {
            this.communicationsContext = communicationsContext;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public void Connect(PeerEndPoint peerEndPoint)
        {
            this.ListenerSafeBlockRawQueue();

            this.RetrieveBlocks(peerEndPoint);
        }

        private void ListenerSafeBlockRawQueue()
        {
            var calculateStatsEvery = 100;
            var bReset = true;
            var blocksPersisted = 0;

            DateTime startTimestamp = DateTime.MinValue;
            DateTime stopTimestamp = DateTime.MinValue;

            var blockchainDbContext = new BlockchainContext();

            Task.Run(() =>
            {
                while (this.retrievingBlocks || this.safeBlockRawQueue.Count > 0)
                {
                    if (this.retrievingBlocks)
                    {
                        this.safeBlockRawQueue.WaitForQueueToChange();
                    }

                    if (bReset)
                    {
                        startTimestamp = DateTime.Now;
                        bReset = false;
                    }

                    var blockRaw = this.safeBlockRawQueue.Dequeue();

                    var blockInDb = this.dbContext
                        .Set<BlockRaw>()
                        .SingleOrDefault(x => x.Index ==  blockRaw.Index);

                    if (blockInDb == null)
                    {
                        this.dbContext.Add(blockRaw);
                        this.dbContext.SaveChanges();

                        blocksPersisted++;
                    }

                    if (blocksPersisted % calculateStatsEvery == 0)
                    {
                        stopTimestamp = DateTime.Now;
                        var processingTime = (stopTimestamp - startTimestamp).TotalSeconds;

                        this.logger.LogInformation($"Blocks persisted {blocksPersisted}: {processingTime} with average {processingTime / calculateStatsEvery}s");
                        bReset = true;
                    }
                }

                if (blocksPersisted % calculateStatsEvery != 0)
                {
                    stopTimestamp = DateTime.Now;
                    var processingTime = (stopTimestamp - startTimestamp).TotalSeconds;

                    this.logger.LogInformation($"Blocks persisted {blocksPersisted}: {processingTime} with average {processingTime / calculateStatsEvery}s");
                    bReset = true;
                }

                Environment.Exit(0);
            });
        }

        private void RetrieveBlocks(PeerEndPoint peerEndPoint)
        {
            this.httpUrl = peerEndPoint.ToString();

            this.peerBlockCount = this.GetBlockCount();
            if (this.peerBlockCount > this.communicationsContext.NetworkConfiguration.ImportTo)
            {
                this.peerBlockCount = this.communicationsContext.NetworkConfiguration.ImportTo;
            }
            else
            {
                return;
            }

            var nextBlockToBeProcessed = this.communicationsContext.NetworkConfiguration.ImportFrom;

            var blocksInRange = this.dbContext
                .Set<BlockRaw>()
                .Where(x =>
                    x.Index > this.communicationsContext.NetworkConfiguration.ImportFrom &&
                    x.Index <= this.communicationsContext.NetworkConfiguration.ImportTo);

            if (blocksInRange.Count() > 0)
            {
                var lastBlockRaw = blocksInRange.Max(x => x.Index);
                nextBlockToBeProcessed = lastBlockRaw + 1;
            }

            if (this.peerBlockCount == nextBlockToBeProcessed)
            {
                return;
            }

            var parallelRetrieves = 100;

            var rounds = (int) Math.Floor(((double)this.peerBlockCount - nextBlockToBeProcessed) / (double)parallelRetrieves);

            var remaind = (this.peerBlockCount - nextBlockToBeProcessed) % parallelRetrieves;

            int lastBlock = 0;
            for (int i = 0; i < rounds; i++)
            {
                var from = nextBlockToBeProcessed + i * parallelRetrieves;
                lastBlock = nextBlockToBeProcessed + (i + 1) * parallelRetrieves;

                Parallel.For(from, lastBlock, index =>
                {
                    var rawBlock = this.RetrieveRawBlock(index);
                    this.safeBlockRawQueue.Enqueue(rawBlock);
                });
            }

            if (remaind != 0)
            {
                var from = nextBlockToBeProcessed + rounds * parallelRetrieves;

                Parallel.For(from, this.peerBlockCount, index =>
                {
                    var rawBlock = this.RetrieveRawBlock(index);
                    this.safeBlockRawQueue.Enqueue(rawBlock);
                });
            }

            this.retrievingBlocks = false;
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

        private BlockRaw RetrieveRawBlock(int blockIndex)
        {
            var rawBlock = this.GetRawBlock(blockIndex);
            var block = JsonConvert.DeserializeObject<Block>(rawBlock);

            return new BlockRaw
            {
                BlockHash = block.Hash,
                Index = block.Index,
                RawData = rawBlock
            };
        }

        //private Block RetrieveBlock(int blockIndex)
        //{
        //    var rawBlock = this.GetRawBlock(blockIndex);
        //    var block = JsonConvert.DeserializeObject<Block>(rawBlock);

        //    foreach (var blockTransaction in block.Transactions)
        //    {
        //        blockTransaction.BlockHash = block.Hash;

        //        if (blockTransaction.Asset != null)
        //        {
        //            blockTransaction.Asset.TransactionHash = blockTransaction.Hash;
        //            blockTransaction.Asset.Transaction = blockTransaction;
        //        }

        //        foreach (var transactionAttribute in blockTransaction.Attributes)
        //        {
        //            transactionAttribute.Transaction = blockTransaction;
        //            transactionAttribute.TransactionHash = blockTransaction.Hash;
        //        }

        //        foreach (var blockTransactionOutput in blockTransaction.Outputs)
        //        {
        //            blockTransactionOutput.Transaction = blockTransaction;
        //            blockTransactionOutput.TransactionHash = blockTransaction.Hash;
        //            blockTransactionOutput.Id = this.transactionOutputId;

        //            this.transactionOutputId++;
        //        }

        //        foreach (var transactionInput in blockTransaction.Inputs)
        //        {
        //            transactionInput.Transaction = blockTransaction;
        //            transactionInput.TransactionHash = blockTransaction.Hash;
        //        }

        //        if (blockTransaction.Claims != null)
        //        {
        //            foreach (var transactionClaim in blockTransaction.Claims)
        //            {
        //                transactionClaim.Transaction = blockTransaction;
        //                transactionClaim.TransactionHash = blockTransaction.Hash;
        //            }
        //        }

        //        foreach (var transactionWitness in blockTransaction.Scripts)
        //        {
        //            transactionWitness.Transaction = blockTransaction;
        //            transactionWitness.Hash = blockTransaction.Hash;
        //        }

        //        if (blockTransaction.Contract != null)
        //        {
        //            blockTransaction.Contract.Transaction = blockTransaction;
        //            blockTransaction.Contract.Hash = blockTransaction.Hash;
        //        }
        //    }

        //    return block;
        //}

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
                var result = client.UploadString(this.httpUrl, serializedRequest);

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
