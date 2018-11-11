using NeoSharp.Communications.Messages.Payloads;

namespace NeoSharp.Communications.Messages
{
    public class VersionMessage : Message<VersionPayload>
    {
        public VersionMessage()
        {
            this.Command = MessageCommand.version;
            this.Payload = new VersionPayload();
        }

        public VersionMessage(VersionPayload versionPayload)
        {
            this.Command = MessageCommand.version;
            this.Payload = versionPayload;
        }
    }
}