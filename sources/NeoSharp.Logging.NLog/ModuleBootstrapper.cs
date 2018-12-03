using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharp.Logging.NLog
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.Register<ILogProvider, NLogProvider>();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}