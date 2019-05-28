using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NeoSharpLight.RPC.BlockchainExtraction.Storage;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    public class BlockchainExtractor : IBlockchainExtractor
    {
        private readonly IAppContext appContext;
        private readonly IStorageAccess _storageAccess;
        private ILogger<BlockchainExtractor> _logger;

        public BlockchainExtractor(
            IAppContext appContext,
            IStorageAccess storageAccess,
            ILogger<BlockchainExtractor> logger)
        {
            this.appContext = appContext;
            this._storageAccess = storageAccess;
            this._logger = logger;
        }

        public Task Start()
        {
            this._logger.LogDebug("Start extraction");

            this._logger.LogDebug(this.appContext.ExtractionConfiguration.RpcPeer);

            this._logger.LogTrace($"Last block processed: {this._storageAccess.GetParameter("lastBlock")}");
            return Task.CompletedTask;
        }
    }
}