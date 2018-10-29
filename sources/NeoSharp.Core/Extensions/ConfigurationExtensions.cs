using System;
using Microsoft.Extensions.Configuration;

namespace NeoSharp.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TConfiguration LoadConfiguration<TConfiguration>(this IConfiguration configuration)
        {
            var configurator = (TConfiguration)Activator.CreateInstance(typeof(TConfiguration), configuration);
            return configurator;
        }
    }
}