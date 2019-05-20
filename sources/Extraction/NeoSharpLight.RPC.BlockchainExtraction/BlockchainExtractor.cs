using System;
using System.Threading.Tasks;
using Hanssens.Net;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    public class BlockchainExtractor
    {
        public Task Start()
        {
            var storage = new LocalStorage();
            // storage.Store("lastBlock", "0");
            // storage.Persist();

            Console.WriteLine("Start extraction");

            Console.WriteLine($"--> {storage.Get("lastBlock")}");
            return Task.CompletedTask;
        }
    }
}