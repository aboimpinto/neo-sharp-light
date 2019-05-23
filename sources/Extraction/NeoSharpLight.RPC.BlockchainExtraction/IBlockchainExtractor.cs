using System.Threading.Tasks;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    public interface IBlockchainExtractor
    {
        Task Start();
    }
}