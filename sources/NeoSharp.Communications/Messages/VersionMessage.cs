using System.IO;
using NeoSharp.Communications.Messages.Payloads;
using NeoSharp.Serialization;

namespace NeoSharp.Communications.Messages
{
    public class VersionMessage : IMessage<VersionPayload>, ISerializable<VersionPayload>, IDeserializable<VersionPayload>
    {
        private readonly IBinarySerializer binarySerializer;

        public MessageFlag Flags { get; set; }

        public MessageCommand Command { get; set; }

        public VersionPayload Payload { get; }

        public VersionMessage(IBinarySerializer binarySerializer)
        {
            this.binarySerializer = binarySerializer;

            this.Command = MessageCommand.version;
            this.Payload = new VersionPayload();
        }

        public byte[] Serialize(VersionPayload payload)
        {
            return this.binarySerializer.Serialize(payload);
        }

        public VersionPayload Deserialize(BinaryReader binaryReader)
        {
            throw new System.NotImplementedException();
        }
    }
}