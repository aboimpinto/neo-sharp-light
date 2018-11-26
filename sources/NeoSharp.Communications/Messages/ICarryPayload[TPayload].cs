namespace NeoSharp.Communications.Messages
{
    public interface ICarryPayload<out TPayload> : ICarryPayload
    {
        TPayload Payload { get; }
    }
}