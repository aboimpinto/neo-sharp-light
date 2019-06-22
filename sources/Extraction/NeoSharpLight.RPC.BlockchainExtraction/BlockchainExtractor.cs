using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NeoSharpLight.RPC.BlockchainExtraction.Node;
using NeoSharpLight.RPC.BlockchainExtraction.Storage;

namespace NeoSharpLight.RPC.BlockchainExtraction
{
    public class BlockchainExtractor : IBlockchainExtractor
    {
        private readonly IAppContext _appContext;
        private readonly IStorageAccess _storageAccess;
        private readonly INodeManager _nodeManager;
        private ILogger<BlockchainExtractor> _logger;

        public BlockchainExtractor(
            IAppContext appContext,
            IStorageAccess storageAccess,
            INodeManager nodeManager,
            ILogger<BlockchainExtractor> logger)
        {
            this._appContext = appContext;
            this._storageAccess = storageAccess;
            this._nodeManager = nodeManager;
            this._logger = logger;
        }

        public Task Start()
        {
            this._logger.LogDebug("Start extraction");

            this._logger.LogDebug($"Get BlockCountConnect on the node {this._appContext.ExtractionConfiguration.RpcPeer}");
            var blockCountOnTarget = this._nodeManager.GetBlockCount();
            this._logger.LogTrace($"BlockCount on node {this._appContext.ExtractionConfiguration.RpcPeer}: {blockCountOnTarget}");

            this._logger.LogTrace($"Last block processed: {this._storageAccess.GetParameter("lastBlock")}");
            return Task.CompletedTask;
        }
    }
}