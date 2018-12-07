using System;
using NeoSharpLight.Core;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            new Bootstrap()
                .Start();

            Console.ReadLine();
        }
    }
}
