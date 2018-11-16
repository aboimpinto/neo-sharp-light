using System;
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
        private const int MaxBufferSize = 4096;

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

        public async Task<Message> ReceiveMessageAsync(Stream stream, CancellationToken cancellationToken)
        {
            var messsageBuffer = await this
                .FillBufferAsync(stream, 24, cancellationToken)
                .ConfigureAwait(false);

            using (var memory = new MemoryStream(messsageBuffer, false))
            {
                using (var reader = new BinaryReader(memory, Encoding.UTF8))
                {
                    var nodeMagic = reader.ReadUInt32();

                    var command = Enum.Parse(typeof(MessageCommand), Encoding.UTF8.GetString(reader.ReadBytes(12)).TrimEnd('\0'));
                }
            }

            return null;
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
                        System.Array.Empty<byte>();

                    binaryWritter.Write((uint)payloadBuffer.Length);
                     binaryWritter.Write(this.crypto.Checksum(payloadBuffer));
                    binaryWritter.Write(payloadBuffer);
                    binaryWritter.Flush();

                    var buffer = memoryStream.ToArray();
                    await memoryStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        private async Task<byte[]> FillBufferAsync(
            Stream stream,
            int size,
            CancellationToken cancellationToken)
        {
            var buffer = new byte[Math.Min(size, MaxBufferSize)];

            using (var memory = new MemoryStream())
            {
                while (size > 0)
                {
                    var count = Math.Min(size, buffer.Length);

                    count = await stream.ReadAsync(buffer, 0, count, cancellationToken).ConfigureAwait(false);
                    if (count <= 0) throw new IOException();

                    memory.Write(buffer, 0, count);
                    size -= count;
                }

                return memory.ToArray();
            }
        }
    }
}