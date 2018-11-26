using System;

namespace NeoSharp.Communications.Messages
{
    public class MessageFactory : IMessageFactory
    {
        private readonly Func<MessageCommand, IMessage> funcFactory;

        public MessageFactory(Func<MessageCommand, IMessage> funcFactory)
        {
            this.funcFactory = funcFactory;
        }

        public IMessage Create(MessageCommand messageCommand)
        {
            return this.funcFactory(messageCommand);
        }
    }

    public interface IMessageFactory
    {
        IMessage Create(MessageCommand messageCommand);
    }
}
