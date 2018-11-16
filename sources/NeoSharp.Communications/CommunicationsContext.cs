using System;
using System.Reflection;
using NeoSharp.Communications.Extensions;
using NeoSharp.Communications.Messages.Payloads;
using NeoSharp.Core;
using NeoSharp.Core.Extensions;

namespace NeoSharp.Communications
{
    public class CommunicationsContext : ICommunicationsContext
    {
        public NetworkConfiguration NetworkConfiguration { get; private set; }

        public VersionPayload VersionPayload 
        {
            get 
            {
                return new VersionPayload
                {
                    Version = 0,
                    Services = 1,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Port = this.NetworkConfiguration.Port,
                    Nonce = (uint)new Random(Environment.TickCount).Next(),
                    UserAgent = $"/NEO-Sharp-Light:{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}/",
                    CurrentBlockIndex = 0,
                    Relay = true
                }; 
            } 
        }

        public CommunicationsContext(INeoSharpContext neoSharpContext)
        {
            this.NetworkConfiguration = neoSharpContext.ApplicationConfiguration.LoadConfiguration<NetworkConfiguration>();
        }

    }
}