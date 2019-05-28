using Microsoft.Extensions.Configuration;
using NeoSharpLight.RPC.BlockchainExtraction.Configuration;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    public class AppContext : IAppContext
    {
        private IConfigurationRoot applicationConfiguration;

        public ExtractionConfiguration ExtractionConfiguration { get; }

        public AppContext()
        {
            this.applicationConfiguration = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            this.ExtractionConfiguration = this.applicationConfiguration.LoadConfiguration<ExtractionConfiguration>();
        }
    }
}