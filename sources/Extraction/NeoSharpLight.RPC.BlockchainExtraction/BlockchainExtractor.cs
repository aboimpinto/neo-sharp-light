using System;
using System.Threading.Tasks;
using Hanssens.Net;
using Microsoft.Extensions.Logging;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    public class BlockchainExtractor : IBlockchainExtractor
    {
        private ILogger<BlockchainExtractor> _logger;

        public BlockchainExtractor(ILogger<BlockchainExtractor> logger)
        {
            this._logger = logger;
        }

        public Task Start()
        {
            var storage = new LocalStorage();
            // storage.Store("lastBlock", "0");
            // storage.Persist();

            this._logger.LogInformation($"--> write information line...");

            Console.WriteLine("Start extraction");

            Console.WriteLine($"--> {storage.Get("lastBlock")}");
            return Task.CompletedTask;
        }
    }
}