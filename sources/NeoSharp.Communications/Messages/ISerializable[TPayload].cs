namespace NeoSharp.Communications.Messages
{
    public interface ISerializable<in TPayload>
    {
        byte[] Serialize(TPayload payload);
    }
}
