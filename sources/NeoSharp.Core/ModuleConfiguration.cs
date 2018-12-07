using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace NeoSharpLight.Core
{
    public class ModuleConfiguration
    {
        public IEnumerable<string> Modules { get; set; }

        public ModuleConfiguration(IConfiguration configuration)
        {
            var section = configuration
                .GetSection("NeoSharpModules");

            section.Bind(this);
        }
    }
}