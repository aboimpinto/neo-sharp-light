using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NeoSharp.Communications.Messages;

namespace NeoSharp.Communications.Protocols
{
    public interface IProtocol
    {
        uint Version { get; }

        bool IsDefault { get; }

        Task SendMessageAsync(Stream stream, Message message, CancellationToken cancellationToken);

         Task<Message> ReceiveMessageAsync(Stream stream, CancellationToken cancellationToken);
    }
}