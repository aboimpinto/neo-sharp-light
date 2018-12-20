using NeoSharp.Core;
using NeoSharp.Core.Extensions;
using StackExchange.Redis;

namespace NeoSharpLight.RPC.NodeManager.RedisDumpDb
{
    public class DbAccess : IDbAccess
    {
        private const string BlockCountKey = "blockCount";

        private readonly IDatabase redisDb;

        public DbAccess(INeoSharpContext neoSharpContext)
        {
            var redisDbConfiguration = neoSharpContext.ApplicationConfiguration
                .LoadConfiguration<RedisDbConfiguration>();

            var redisConnectionMultiplexer = ConnectionMultiplexer.Connect(redisDbConfiguration.Server);
            this.redisDb = redisConnectionMultiplexer.GetDatabase(redisDbConfiguration.Instance);
        }

        public int GetBlockCount()
        {
            int.TryParse(this.redisDb.StringGet(BlockCountKey), out var blockCount);

            return blockCount;
        }

        public void SaveBlock(int index, string block)
        {
            this.redisDb.StringSet(index.ToString(), block);
        }

        public void SaveBlockCount(int index)
        {
            var currentBlockCount = this.GetBlockCount();

            // Only save if the index is higher the persisted blockCount
            if (index > currentBlockCount)
            {
                this.redisDb.StringSet(BlockCountKey, index.ToString());
            }
        }
    }
}
