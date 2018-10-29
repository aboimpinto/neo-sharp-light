using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using NeoSharp.Communications.Extensions;

namespace NeoSharp.Communications
{
    public class PeerEndPoint : IPeerEndPoint
    {
        private readonly Regex endPointPattern = new Regex(@"^(?<proto>\w+)://(?<address>[^/]+)/?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public NodeProtocol NodeProtocol { get; private set; }

        public string Host { get; private set; }

        public IPAddress IpAddress { get; private set; }
        
        public int Port { get; private set; }

        public PeerEndPoint(string endPoint)
        {
            var match = this.endPointPattern.Match(endPoint);
            if (!match.Success)
            {
                throw new ArgumentException("Invalid {value} endpoint.");
            }

            var protocol = match.Groups["proto"].MatchGroupValue();
            var address = match.Groups["address"].MatchGroupValue();

            if (!Enum.TryParse(protocol, out NodeProtocol protocolEnum))
            {
                protocolEnum = NodeProtocol.Unknown;
            }
            this.NodeProtocol = protocolEnum;

             var host = address;
            var port = 0;

            var portIndex = address.LastIndexOf(':');
            if (portIndex > 0 &&
                portIndex < address.Length - 1 &&
                address[portIndex - 1] != ':')
            {
                host = address.Substring(0, portIndex);
                port = int.Parse(address.Substring(portIndex + 1), CultureInfo.InvariantCulture);
            }

            this.Host = host;
            this.Port = port;

            this.GetIPAddress();
        }

        private void GetIPAddress()
        {
            if (IPAddress.TryParse(this.Host, out var ipAddress))
            {
                this.IpAddress = ipAddress;
            }

            try
            {
                // var ipHostEntry = await Dns.GetHostEntryAsync(this.Host);
                var getHostEntryAsyncTask = Dns.GetHostEntryAsync(this.Host);
                getHostEntryAsyncTask.Wait();

                var ipHostEntry = getHostEntryAsyncTask.Result;

                this.IpAddress = ipHostEntry
                    .AddressList
                    .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork || x.IsIPv6Teredo);
            }
            catch (System.Exception)
            {
                
                this.IpAddress = null;
            }
        }
    }
}