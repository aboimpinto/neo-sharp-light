using System;
using NeoSharp.Core;
using NeoSharp.Core.Extensions;

namespace NeoSharp.Communications
{
    public class NodeConnector : INodeConnector
    {
        public NodeConnector(INeoSharpContext neoSharpContext)
        {
            var networkConfiguration = neoSharpContext.ApplicationConfiguration.LoadConfiguration<NetworkConfiguration>();
        }

        public void Connect()
        {
            
        }
    }
}