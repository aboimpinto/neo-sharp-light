using Microsoft.Extensions.Configuration;

namespace NeoSharp.Core
{
    public interface INeoSharpContext
    {
         IConfigurationRoot ApplicationConfiguration { get; set; }
    }
}