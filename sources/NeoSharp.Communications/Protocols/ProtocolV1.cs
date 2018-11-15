using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NeoSharp.Communications.Messages;
using NeoSharp.Cryptography;
using NeoSharp.Serialization;

namespace NeoSharp.Communications.Protocols
{
    public class ProtocolV1 : IProtocol
    {
        private readonly ICommunicationsContext communicationsContext;
        private readonly ICrypto crypto;
        private readonly IBinarySerializer serializer;

        public ProtocolV1(
            ICommunicationsContext communicationsContext,
            ICrypto crypto, 
            IBinarySerializer serializer)
        {
            this.communicationsContext = communicationsContext;
            this.crypto = crypto;
            this.serializer = serializer;
        }

        public uint Version => 1;

        public bool IsDefault => true;

        public Task<Message> ReceiveMessageAsync(Stream stream, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendMessageAsync(Stream stream, Message message, CancellationToken cancellationToken)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWritter = new BinaryWriter(memoryStream, Encoding.UTF8))
                {
                    binaryWritter.Write(this.communicationsContext.NetworkConfiguration.Magic);
                    binaryWritter.Write(Encoding.UTF8.GetBytes(message.Command.ToString().PadRight(12, '\0')));

                    // write the payload
                    var payloadBuffer = message is ICarryPayload messageWithPayload ?
                        this.serializer.Serialize(messageWithPayload.Payload) : 
                        new byte[0];

                    // var payloadBuffer = new byte[0];

                    binaryWritter.Write((uint)payloadBuffer.Length);
                     binaryWritter.Write(this.crypto.Checksum(payloadBuffer));
                    binaryWritter.Write(payloadBuffer);
                    binaryWritter.Flush();

                    var buffer = memoryStream.ToArray();
                    await memoryStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }
}