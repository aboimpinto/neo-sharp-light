using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharp.Logging.NLog
{
    public class ModuleBootstraper : IModuleBootstrapper
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