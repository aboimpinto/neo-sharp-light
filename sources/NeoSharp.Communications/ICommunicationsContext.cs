using NeoSharp.Communications.Messages.Payloads;

namespace NeoSharp.Communications
{
    public interface ICommunicationsContext
    {
        NetworkConfiguration NetworkConfiguration { get; }

         VersionPayload VersionPayload { get; }
    }
}