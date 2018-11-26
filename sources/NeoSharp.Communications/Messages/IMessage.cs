namespace NeoSharp.Communications.Messages
{
    public interface IMessage
    {
        /// <summary>
        /// Flags
        /// </summary>
        MessageFlag Flags { get; set; }

        /// <summary>
        /// Command
        /// </summary>
        MessageCommand Command { get; set; }
    }
}
