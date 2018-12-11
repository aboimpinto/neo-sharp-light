using NeoSharp.Core;
using NeoSharp.Core.Extensions;
using StackExchange.Redis;

namespace NeoSharpLight.RPC.NodeManager.RedisDumpDb
{
    public class DbAccess : IDbAccess
    {
        private readonly ExtractionConfiguration extractionConfiguration;
        private IDatabase redisDb;

        public DbAccess(INeoSharpContext neoSharpContext)
        {
            this.extractionConfiguration = neoSharpContext.ApplicationConfiguration.LoadConfiguration<ExtractionConfiguration>();

            var redisConnectionMultiplexer = ConnectionMultiplexer.Connect("localhost");
            this.redisDb = redisConnectionMultiplexer.GetDatabase(0);
        }

        public int GetBlockCount()
        {
            int.TryParse(this.redisDb.StringGet("blockCount"), out int blockCount);

            return blockCount;
        }

        public void SaveBlock(int index, string block)
        {
            this.redisDb.StringSet(index.ToString(), block);
        }

        public void SaveBlockCount(int index)
        {
            this.redisDb.StringSet("blockCount", index.ToString());
        }
    }
}
