using Microsoft.Extensions.Configuration;

namespace NeoSharp.Core
{
    public class ApplicationEntryPointConfiguration
    {
        public string EntryPoint { get; set; }
        public ApplicationEntryPointConfiguration(IConfiguration configuration)
        {
            var section = configuration
                .GetSection("ApplicationEntryPointConfiguration");

            section.Bind(this);
        }
    }
}
