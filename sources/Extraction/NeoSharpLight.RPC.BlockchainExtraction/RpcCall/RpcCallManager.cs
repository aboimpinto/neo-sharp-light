using System;
using System.Net;
using Newtonsoft.Json;

namespace NeoSharpLight.RPC.BlockchainExtraction.RpcCall
{
    public class RpcCallManager : IRpcCallManager
    {
        private readonly IAppContext context;

        public RpcCallManager(IAppContext context)
        {
            this.context = context;
        }

        public dynamic RpcCall(RpcCallRequest request)
        {
            using (var client = new WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var serializedRequest = JsonConvert.SerializeObject(request);
                var result = client.UploadString(this.context.ExtractionConfiguration.RpcPeer, serializedRequest);

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