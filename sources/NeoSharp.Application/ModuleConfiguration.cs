using Microsoft.Extensions.Configuration;

namespace NeoSharp.Application
{
    public class ModuleConfiguration
    {
        public string[] Modules { get; set; }

        public ModuleConfiguration(IConfiguration configuration)
        {
            var section = configuration
                .GetSection("NeoSharpModules");

            section.Bind(this);
        }

    }
}