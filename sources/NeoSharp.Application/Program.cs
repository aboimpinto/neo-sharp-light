using System;

namespace NeoSharp.Application
{
    public static class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1801", Justification="Main args")]
        public static void Main(string[] args)
        {
            var bootstraper = new Bootstrap();
            bootstraper.Start();

            Console.ReadLine();
        }
    }
}
