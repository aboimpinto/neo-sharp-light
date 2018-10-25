using System;

namespace NeoSharp.Application
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var bootstraper = new Bootstrap();
            bootstraper.LoadModules();

            Console.ReadLine();
        }
    }
}
