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
        private readonly INodeAccess nodeAccess;
        private readonly IDbAccess dbAccess;
        private readonly ILogger<ExtractionManager> logger;
        private readonly ExtractionConfiguration extractionConfiguration;

        public ExtractionManager(
            INeoSharpContext neoSharpContext,
            INodeAccess nodeAccess, 
            IDbAccess dbAccess, 
            ILogger<ExtractionManager> logger)
        {
            this.nodeAccess = nodeAccess;
            this.dbAccess = dbAccess;
            this.logger = logger;

            this.extractionConfiguration = neoSharpContext.ApplicationConfiguration.LoadConfiguration<ExtractionConfiguration>();
        }

        public void StartExtraction()
        {
            var remoteBlockCount = this.nodeAccess.GetBlockCount();
            var localBlockCount = this.dbAccess.GetBlockCount();

            if (this.extractionConfiguration.ImportFrom > 0 || this.extractionConfiguration.ImportTo > 0)
            {
                localBlockCount = this.extractionConfiguration.ImportFrom;
                remoteBlockCount = this.extractionConfiguration.ImportTo;
            }

            if (localBlockCount >= remoteBlockCount)
            {
                Environment.Exit(0);
            }

            var startTimestamp = DateTime.Now;

            for (var i = localBlockCount; i < remoteBlockCount; i++)
            {
                var rawBlock = this.nodeAccess.GetRawBlock(i);

                this.dbAccess.SaveBlock(i, rawBlock.ToString());
                this.dbAccess.SaveBlockCount(i);

                if (i <= 0 || i % 100 != 0) continue;

                var endTimestamp = DateTime.Now;
                var processingTime = (endTimestamp - startTimestamp).TotalSeconds;

                this.logger.LogInformation($"Blocks persisted {i}: {processingTime} with average {processingTime / 100}s");
                startTimestamp = DateTime.Now;
            }
        }
    }
}
