using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NeoSharpLight.RPC.BlockchainExtraction.RemoteProcedureCall;
using NeoSharpLight.RPC.BlockchainExtraction.Storage;
// using NeoSharpLight.Core;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    public class Program
    {
        private static IServiceProvider _serviceProvider;

        private async static Task Main(string[] args)
        {
            // new Bootstrap()
            //     .Start(args);

            RegisterServices();

            var logger = _serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");


            // var extrator = new BlockchainExtractor();
            var extractor = _serviceProvider.GetService<IBlockchainExtractor>();
            await extractor.Start();

            Console.WriteLine();
            Console.WriteLine("End processing new blocks.");

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();

            collection.AddLogging(x =>
            {
                x.AddConsole();
                x.AddDebug();
                x.SetMinimumLevel(LogLevel.Trace);
            });

            // Singletons instances
            collection
                .AddSingleton<IAppContext, AppContext>()
                .AddSingleton<IStorageAccess, StorageAccess>()
                .AddSingleton<IRemoteProcedureCallManager, RemoteProcedureCallManager>();

            // Scoped instances
            collection
                .AddScoped<IBlockchainExtractor, BlockchainExtractor>();

            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }

            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
