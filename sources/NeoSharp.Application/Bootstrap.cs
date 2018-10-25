using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace NeoSharp.Application
{
    public class Bootstrap
    {
        private ModuleConfiguration moduleConfiguration;

        public Bootstrap()
        {
            var neoSharpConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            this.moduleConfiguration = neoSharpConfiguration.LoadConfiguration<ModuleConfiguration>();
        }

        public void LoadModules()
        {
        }
    }
}