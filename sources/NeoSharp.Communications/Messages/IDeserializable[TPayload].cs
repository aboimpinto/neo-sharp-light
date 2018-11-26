using System.IO;

namespace NeoSharp.Communications.Messages
{
    public interface IDeserializable<out TPayload>
    {
        TPayload Deserialize(BinaryReader binaryReader);
    }
}
