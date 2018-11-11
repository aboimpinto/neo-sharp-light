using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NeoSharp.Communications.Messages;
using NeoSharp.Core;

namespace NeoSharp.Communications.Protocols
{
    public class ProtocolV1 : IProtocol
    {
        private readonly INeoSharpContext neoSharpContext;

        public ProtocolV1(INeoSharpContext neoSharpContext)
        {
            this.neoSharpContext = neoSharpContext;
        }

        public uint Version => 1;

        public bool IsDefault => true;

        public Task<Message> ReceiveMessageAsync(Stream stream, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SendMessageAsync(Stream stream, Message message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}