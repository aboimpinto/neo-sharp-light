using System;
using System.Threading.Tasks;
// using NeoSharpLight.Core;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    public class Program
    {
        private async static Task Main(string[] args)
        {
            // new Bootstrap()
            //     .Start(args);

            var extrator = new BlockchainExtractor();
            await extrator.Start();

            Console.WriteLine();
            Console.WriteLine("End processing new blocks.");
        }
    }
}
