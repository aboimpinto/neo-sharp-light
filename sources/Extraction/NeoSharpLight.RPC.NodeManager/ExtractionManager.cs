using System;
using System.Collections.Generic;
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

        public void StartExtraction(string[] args)
        {
            var finalConfiguration = this.GetFinalConfiguration(args);

            if (!string.IsNullOrEmpty(finalConfiguration.PeerAddress))
            {
                this.nodeAccess.OverridePeerAddress(finalConfiguration.PeerAddress);
            }

            var remoteBlockCount = this.nodeAccess.GetBlockCount();
            var localBlockCount = this.dbAccess.GetBlockCount();

            if (finalConfiguration.ImportFrom > 0 || finalConfiguration.ImportTo > 0)
            {
                localBlockCount = finalConfiguration.ImportFrom;
                remoteBlockCount = finalConfiguration.ImportTo;
            }

            this.logger.LogTrace($"Obtaining blocks from {localBlockCount} to {remoteBlockCount} from server {finalConfiguration.PeerAddress}");

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

        private FinalNetworkConfiguration GetFinalConfiguration(IReadOnlyList<string> args)
        {
            if (args.Count == 0)
            {
                return new FinalNetworkConfiguration
                {
                    ImportFrom = this.extractionConfiguration.ImportFrom,
                    ImportTo = this.extractionConfiguration.ImportTo,
                    PeerAddress = this.extractionConfiguration.RpcPeer
                };
            }

            if (args.Count == 3)
            {
                this.logger.LogTrace($"Arg[0]: {args[0]}");
                this.logger.LogTrace($"Arg[1]: {args[1]}");
                this.logger.LogTrace($"Arg[2]: {args[2]}");

                return new FinalNetworkConfiguration
                {
                    ImportFrom = int.Parse(args[0]),
                    ImportTo = int.Parse(args[1]),
                    PeerAddress = args[2]
                };
            }

            throw new InvalidOperationException("Invalid parameters");
        }

        public class FinalNetworkConfiguration
        {
            public int ImportFrom { get; set; }

            public int ImportTo { get; set; }

            public string PeerAddress { get; set; }
        }
    }
}
