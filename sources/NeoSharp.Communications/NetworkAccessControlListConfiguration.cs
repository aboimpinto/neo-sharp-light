namespace NeoSharp.Communications
{
    public class NetworkAccessControlListConfiguration
    {
        /// <summary>
        /// Access control behaviour
        /// </summary>
        public AccessControlType Type { get; set; } = AccessControlType.None;

        /// <summary>
        /// Path of rules file
        /// </summary>
        public string Path { get; set; }
    }
}