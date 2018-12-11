using System;
using NeoSharp.Core;
using NeoSharp.Core.Extensions;
using NeoSharp.Logging;
using NeoSharpLight.RPC.NodeManager.RedisDumpDb;
using NeoSharpLight.RPC.NodeManager.RpcNodeAccess;

namespace NeoSharpLight.RPC.NodeManager
{
    public class ExtractionManager : IExtractionManager
    {
        private readonly INeoSharpContext neoSharpContext;
        private readonly INodeAccess nodeAccess;
        private readonly IDbAccess dbAccess;
        private readonly ILogger<ExtractionManager> logger;

        public ExtractionManager(
            INeoSharpContext neoSharpContext,
            INodeAccess nodeAccess, 
            IDbAccess dbAccess, 
            ILogger<ExtractionManager> logger)
        {
            this.neoSharpContext = neoSharpContext;
            this.nodeAccess = nodeAccess;
            this.dbAccess = dbAccess;
            this.logger = logger;

            var extractionConfiguraction = neoSharpContext.ApplicationConfiguration.LoadConfiguration<ExtractionConfiguration>();
        }

        public void StartExtraction()
        {
            var remoteBlockCount = this.nodeAccess.GetBlockCount();

            var localBlockCount = this.dbAccess.GetBlockCount();

            if (localBlockCount >= remoteBlockCount)
            {
                Environment.Exit(0);
            }

            var startTimestamp = DateTime.Now;
            var endTimestamp = DateTime.Now;

            for (int i = localBlockCount; i < remoteBlockCount; i++)
            {
                var rawBlock = this.nodeAccess.GetRawBlock(i);

                this.dbAccess.SaveBlock(i, rawBlock.ToString());
                this.dbAccess.SaveBlockCount(i);

                if (i > 0 && i % 100 == 0)
                {
                    endTimestamp = DateTime.Now;
                    var processingTime = (endTimestamp - startTimestamp).TotalSeconds;

                    this.logger.LogInformation($"Blocks persisted {i}: {processingTime} with average {processingTime / 100}s");
                    startTimestamp = DateTime.Now;
                }
            }
        }
    }
}
