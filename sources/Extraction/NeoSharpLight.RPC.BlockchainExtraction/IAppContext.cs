using NeoSharpLight.RPC.BlockchainExtraction.Configuration;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    public interface IAppContext
    {
        ExtractionConfiguration ExtractionConfiguration { get; }
    }
}