using System;
using System.Collections.Generic;
using System.Net;
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
        private readonly ILogger<HttpPeer> logger;

        public bool Connected { get; }

        public bool IsReady { get; }

        public HttpPeer(ILogger<HttpPeer> logger)
        {
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

            for (var i = this.lastBlockExtracted; i < this.peerBlockCount - 1; i++)
            {
                var block = this.RetrieveBlock(i);

                this.logger.LogInformation($"Block {block.Hash} with index {block.Index} retrieved.");
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
            return JsonConvert.DeserializeObject<Block>(rawBlock);
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
