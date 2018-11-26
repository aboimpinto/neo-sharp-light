namespace NeoSharp.Communications.Messages
{
    public interface IMessage<out TPayload> : IMessage, ICarryPayload<TPayload>
    {
    }
}
