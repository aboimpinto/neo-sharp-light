using NeoSharp.Communications.Messages;
using NeoSharp.Communications.Messages.Payloads;

namespace NeoSharp.Communications
{
    public interface ICommunicationsContext
    {
        NetworkConfiguration NetworkConfiguration { get; }

         VersionMessage VersionMessage { get; }
    }
}