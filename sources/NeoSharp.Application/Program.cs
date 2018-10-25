using System;

namespace NeoSharp.Application
{    
    class Program
    {
        static void Main(string[] args)
        {
            var bootstraper = new Bootstrap();
            bootstraper.LoadModules();

            Console.ReadLine();
        }
    }
}
