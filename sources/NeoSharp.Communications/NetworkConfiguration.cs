using Microsoft.Extensions.Configuration;

namespace NeoSharp.Communications
{
    public class NetworkConfiguration
    {
        /// <summary>
        /// Magic number
        /// </summary>
        public uint Magic { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        
        public ushort Port { get; set; }

        /// <summary>
        /// Force Ipv6
        /// </summary>
        public bool ForceIPv6 { get; set; }

        /// <summary>
        /// Peers
        /// </summary>
        public string[] Peers { get; set; }
        // public EndPoint[] PeerEndPoints { get; internal set; }

        /// <summary>
        /// Acl Config
        /// </summary>
        public NetworkAccessControlListConfiguration AccessControlList { get; set; }

        /// <summary>
        /// StandByValidator config
        /// </summary>
        public string[] StandByValidator { get; internal set; }

        public NetworkConfiguration(IConfiguration configuration)
        {
            var section = configuration
                .GetSection("Network");

            section.Bind(this);
        }
    }
}