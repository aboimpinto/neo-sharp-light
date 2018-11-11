namespace NeoSharp.Communications.Messages
{
    public class Message<TPayload> : Message, ICarryPayload
        where TPayload : new()
    {
        public object Payload { get; protected set; }

        object ICarryPayload.Payload
        {
            get => Payload;
            set => Payload = (TPayload)value;
        }
    }
}