// using System;
// using System.Collections.Generic;
// using System.Net;
// using NeoSharp.Core;
// using NeoSharp.Core.Extensions;
// using Newtonsoft.Json;

// namespace NeoSharpLight.RPC.NodeManager.RpcNodeAccess
// {
//     public class NodeAccess : INodeAccess
//     {
//         private string rpcPeerUrl;

//         public NodeAccess(INeoSharpContext neoSharpContext)
//         {
//             this.rpcPeerUrl = neoSharpContext.ApplicationConfiguration
//                 .LoadConfiguration<ExtractionConfiguration>()
//                 .RpcPeer;
//         }

//         public int GetBlockCount()
//         {
//             var callResult = this.RpcCall(new JsonRpcRequest
//             {
//                 Method = NeoRpcMethod.getblockcount
//             });

//             return callResult == null ?
//                 0 :
//                 (int)int.Parse(callResult.ToString());
//         }

//         public dynamic GetRawBlock(int index)
//         {
//             var callResult = this.RpcCall(new JsonRpcRequest
//             {
//                 Method = NeoRpcMethod.getblock,
//                 Parameters = new List<object> { index, 1 }
//             });

//             return callResult;
//         }

//         public void OverridePeerAddress(string peerAddress)
//         {
//             this.rpcPeerUrl = peerAddress;
//         }

//         private dynamic RpcCall(JsonRpcRequest request)
//         {
//             using (var client = new WebClient { Encoding = System.Text.Encoding.UTF8 })
//             {
//                 client.Headers[HttpRequestHeader.ContentType] = "application/json";
//                 var serializedRequest = JsonConvert.SerializeObject(request);
//                 var result = client.UploadString(this.rpcPeerUrl, serializedRequest);

//                 var json = (dynamic)JsonConvert.DeserializeObject(result);
//                 if (json != null)
//                 {
//                     if (json.error != null)
//                     {
//                         throw new Exception(json.message);
//                     }
//                     else if (json.result != null)
//                     {
//                         return json.result;
//                     }
//                 }
//             }

//             return null;
//         }
//     }
// }
