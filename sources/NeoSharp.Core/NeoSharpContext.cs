using Microsoft.Extensions.Configuration;

namespace NeoSharp.Core
{
    public class NeoSharpContext : INeoSharpContext
    {
        public IConfigurationRoot ApplicationConfiguration { get; set; }
    }
}