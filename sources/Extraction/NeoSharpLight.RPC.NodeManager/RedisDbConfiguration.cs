using Microsoft.Extensions.Configuration;

namespace NeoSharpLight.RPC.NodeManager
{
    public class RedisDbConfiguration
    {
        public string Server { get; set; }

        public int Instance { get; set; }

        public RedisDbConfiguration(IConfiguration configuration)
        {
            var section = configuration.GetSection("RedisDbConfiguration");
            section.Bind(this);
        }
    }
}
