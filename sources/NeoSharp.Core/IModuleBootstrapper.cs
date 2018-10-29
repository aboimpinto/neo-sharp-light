
using Microsoft.Extensions.Configuration;
using NeoSharp.DependencyInjection;

namespace NeoSharp.Core
{
    /// <summary>
    /// Interface that define the module boostrapping.
    /// </summary>
    public interface IModuleBootstrapper
    {
        /// <summary>
        /// Starts the module.
        /// </summary>
         void Start(IContainer container);

        /// <summary>
        /// Stops the module.
        /// </summary>
         void Stop();
    }
}