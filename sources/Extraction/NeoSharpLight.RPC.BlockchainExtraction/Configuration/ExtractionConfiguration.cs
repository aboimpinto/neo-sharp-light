using Microsoft.Extensions.Configuration;

namespace NeoSharpLight.RPC.BlockchainExtraction.Configuration
{
    public class ExtractionConfiguration
    {
        public string RpcPeer { get; set; }

        public ExtractionConfiguration(IConfiguration configuration)
        {
            var section = configuration.GetSection("ExtractionConfiguration");
            section.Bind(this);
        }
    }
}