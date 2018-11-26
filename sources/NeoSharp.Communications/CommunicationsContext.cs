using System;
using System.Reflection;
using NeoSharp.Communications.Extensions;
using NeoSharp.Communications.Messages;
using NeoSharp.Core;
using NeoSharp.Core.Extensions;

namespace NeoSharp.Communications
{
    public class CommunicationsContext : ICommunicationsContext
    {
        private readonly IMessageFactory messageFactory;

        public NetworkConfiguration NetworkConfiguration { get; private set; }

        public VersionMessage VersionMessage
        {
            get
            {
                var versionMessage = (VersionMessage)this.messageFactory.Create(MessageCommand.version);
                versionMessage.Payload.Version = 0;
                versionMessage.Payload.Services = 1;
                //versionMessage.Payload.Timestamp = DateTime.UtcNow.ToTimestamp();
                versionMessage.Payload.Timestamp = 1543028756;
                versionMessage.Payload.Port = this.NetworkConfiguration.Port;
                //versionMessage.Payload.Nonce = (uint)new Random(Environment.TickCount).Next();
                versionMessage.Payload.Nonce = 254912708;
                versionMessage.Payload.UserAgent = $"/NEO-Sharp:{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}/";
                versionMessage.Payload.CurrentBlockIndex = 0;
                versionMessage.Payload.Relay = true;

                return versionMessage;
            } 
        }

        public CommunicationsContext(
            INeoSharpContext neoSharpContext,
            IMessageFactory messageFactory)
        {
            this.NetworkConfiguration = neoSharpContext.ApplicationConfiguration.LoadConfiguration<NetworkConfiguration>();
            this.messageFactory = messageFactory;
        }

    }
}