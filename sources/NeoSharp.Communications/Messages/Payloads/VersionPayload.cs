namespace NeoSharp.Communications.Messages.Payloads
{
    public class VersionPayload
    {
        public uint Version { get; set; }

        public ulong Services { get; set; }

        public uint Timestamp{ get; set; }

        public ushort Port { get; set; }

        public uint Nonce { get; set; }

        public string UserAgent { get; set; }

        public uint CurrentBlockIndex { get; set; }

        public bool Relay { get; set; }
    }
}