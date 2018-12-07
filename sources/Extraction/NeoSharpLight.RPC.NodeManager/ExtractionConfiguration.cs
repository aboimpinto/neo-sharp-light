using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace NeoSharpLight.RPC.NodeManager
{
    public class ExtractionConfiguration
    {
        public int ImportFrom { get; set; }

        public int ImportTo { get; set; }

        public string RpcPeer { get; set; }

        public ExtractionConfiguration(IConfiguration configuration)
        {
            var section = configuration.GetSection("ExtractionConfiguration");
            section.Bind(this);
        }
    }
}
