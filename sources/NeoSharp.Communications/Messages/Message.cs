namespace NeoSharp.Communications.Messages
{
    public class Message
    {
        /// <summary>
        /// Max size for payload
        /// </summary>
        public const int PayloadMaxSize = 0x02000000;

        /// <summary>
        /// Flags
        /// </summary>
        public MessageFlag Flags { get; set; }

        /// <summary>
        /// Command
        /// </summary>
        public MessageCommand Command { get; set; }
    }
}