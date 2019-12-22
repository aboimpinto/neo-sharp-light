using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace NeoBlockchainExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            var logMinumumLevel = Serilog.Events.LogEventLevel.Verbose;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile("extrator.log", logMinumumLevel, retainedFileCountLimit:7)
                .WriteTo.Console(logMinumumLevel)
                .CreateLogger();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation(".... Starting NeoBlockchainExtractor ....");
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(x => 
                {
                    x.AddSerilog();
                });
        }
    }
}
