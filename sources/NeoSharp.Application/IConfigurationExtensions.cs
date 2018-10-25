using System;
using Microsoft.Extensions.Configuration;

namespace NeoSharp.Application
{
    public static class IConfigurationExtensions
    {
        public static TConfiguration LoadConfiguration<TConfiguration>(this IConfiguration configuration)
        {
            var configurator = (TConfiguration)Activator.CreateInstance(typeof(TConfiguration), configuration);
            return configurator;
        }
    }
}