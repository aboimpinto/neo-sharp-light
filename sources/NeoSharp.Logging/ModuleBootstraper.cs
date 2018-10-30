using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharp.Logging
{
    public class ModuleBootstraper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
             container.Register(typeof(ILogger<>), typeof(Logger<>));
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}