using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NeoSharp.Communications.Extensions;
using NeoSharp.Communications.Messages;
using NeoSharp.Cryptography;
using NeoSharp.Logging;

namespace NeoSharp.Communications.Protocols
{
    public class ProtocolV1 : IProtocol
    {
        private readonly ICommunicationsContext communicationsContext;
        private readonly IMessageFactory messageFactory;
        private readonly ICrypto crypto;
        private readonly ILogger<ProtocolV1> logger;

        public ProtocolV1(
            ICommunicationsContext communicationsContext,
            IMessageFactory messageFactory,
            ICrypto crypto, 
            ILogger<ProtocolV1> logger)
        {
            this.communicationsContext = communicationsContext;
            this.messageFactory = messageFactory;
            this.crypto = crypto;
            this.logger = logger;
        }

        public uint Version => 1;

        public bool IsDefault => true;

        public async Task<Message> ReceiveMessageAsync(Stream stream, CancellationToken cancellationToken)
        {
            var messageBuffer = await stream.FillBufferAsync(24, cancellationToken)
                .ConfigureAwait(false);

            using (var memory = new MemoryStream(messageBuffer, false))
            {
                using (var reader = new BinaryReader(memory, Encoding.UTF8))
                {
                    var nodeMagic = reader.ReadUInt32();
                    if (nodeMagic != this.communicationsContext.NetworkConfiguration.Magic)
                    {
                        throw new FormatException();
                    }

                    var commandBytes = reader.ReadBytes(12);
                    var command = (MessageCommand) Enum.Parse(typeof(MessageCommand), Encoding.UTF8.GetString(commandBytes).TrimEnd('\0'));

                    var payloadLength = reader.ReadUInt32();
                    var payloadChecksum = reader.ReadUInt32();

                    var payloadBuffer = payloadLength > 0 
                        ? await stream.FillBufferAsync((int)payloadLength, cancellationToken)
                            .ConfigureAwait(false)
                        : Array.Empty<byte>();

                    var isPayloadValid = this.crypto.Checksum(payloadBuffer) == payloadChecksum;

                    if (!isPayloadValid)
                    {
                        this.logger.LogError($"Message checksum invalid {this.crypto.Checksum(payloadBuffer)} != {payloadChecksum}.");
                        return null;
                    }

                    var message = this.messageFactory.Create(command);

                    //var isMessageWithPayload = message.GetType()
                    //    .GetInterfaces()
                    //    .Any(x =>
                    //        x.IsGenericType &&
                    //        x.GetGenericTypeDefinition() == typeof(ICarryPayload<>));
                    //if (isMessageWithPayload)
                    //{

                    //}

                    if (message.GetType().ImplementsGeneric(typeof(ICarryPayload<>)))
                    {
                        var propertyInfo = message.GetType().GetProperty("Payload");
                        if (propertyInfo == null)
                        {
                            this.logger.LogError($"Could not find the 'Payload' field in the message {message.GetType().Name}.");
                            return null;
                        }

                        if (message.GetType().ImplementsGeneric(typeof(IDeserializable<>)))
                        {
                            var deserializedMessage = message.GetType().GetMethod("Deserialize")?.Invoke(message, new object[] { reader });
                            propertyInfo.SetValue(message, deserializedMessage);
                        }
                    }

                    //if (message is ICarryPayload messageWithPayload)
                    //{
                    //}
                }
            }

            return null;
        }

        public async Task SendMessageAsync(Stream stream, IMessage message, CancellationToken cancellationToken)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8))
                {
                    binaryWriter.Write(this.communicationsContext.NetworkConfiguration.Magic);
                    binaryWriter.Write(Encoding.UTF8.GetBytes(message.Command.ToString().PadRight(12, '\0')));

                    // write the payload
                    var payloadBuffer = Array.Empty<byte>();

                    if (message.GetType().ImplementsGeneric(typeof(ICarryPayload<>)))
                    {
                        var propertyInfo = message.GetType().GetProperty("Payload");
                        if (propertyInfo == null)
                        {
                            this.logger.LogError($"Could not find the 'Payload' field in the message {message.GetType().Name}.");
                            return;
                        }

                        if (message.GetType().ImplementsGeneric(typeof(ISerializable<>)))
                        {
                            message.GetType().GetMethod("Serialize")?.Invoke(message, new [] { propertyInfo.GetValue(message) });
                        }
                    }

                    binaryWriter.Write((uint)payloadBuffer.Length);
                    binaryWriter.Write(this.crypto.Checksum(payloadBuffer));
                    binaryWriter.Write(payloadBuffer);
                    binaryWriter.Flush();

                    var buffer = memoryStream.ToArray();
                    await stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }
}