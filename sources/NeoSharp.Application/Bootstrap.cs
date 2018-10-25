using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace NeoSharp.Application
{
    public class Bootstrap
    {
        private IEnumerable<string> moduleToLoad;

        public Bootstrap()
        {
            var neoSharpConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();


            var modulesConfiguration = neoSharpConfiguration.LoadConfiguration<ModuleConfiguration>();
        }

        public void LoadModules()
        {

        }
    }

    public static class IConfigurationExtensions
    {
        public static TConfiguration LoadConfiguration<TConfiguration>(this IConfiguration configuration)
        {
            var configurator = (TConfiguration)Activator.CreateInstance(typeof(TConfiguration), configuration);
            return configurator;
        }
    }

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