namespace NeoSharp.Communications.Messages
{
    public enum MessageFlag 
    {
        /// <summary>
        /// No flag
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Compressed
        /// </summary>
        Compressed = 0x01,
        
        /// <summary>
        /// Urgent
        /// </summary>
        Urgent = 0x02,
        
        /// <summary>
        /// WithPayload
        /// </summary>
        WithPayload = 0x04
    }
}