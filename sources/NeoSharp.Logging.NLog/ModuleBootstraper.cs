using NeoSharp.DependencyInjection;

namespace NeoSharp.Logging.NLog
{
    public class ModuleBootstraper : IModule
    {
        public void Register(IContainerModule containerModule)
        {
            containerModule.Register<ILogProvider, NLogProvider>();
        }
    }
}
