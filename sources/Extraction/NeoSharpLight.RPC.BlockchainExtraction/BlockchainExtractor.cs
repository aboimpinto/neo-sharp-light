using System;
using System.Threading.Tasks;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    public class BlockchainExtractor
    {
        public Task Start()
        {
            Console.WriteLine("Start extraction");
            return Task.CompletedTask;
        }
    }
}