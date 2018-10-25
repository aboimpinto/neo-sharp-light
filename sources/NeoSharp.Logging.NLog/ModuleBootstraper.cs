using NeoSharp.DependencyInjection;

namespace NeoSharp.Logging.NLog
{
    public class ModuleBootstraper : IModule
    {
        public void Register(IContainerBuilder containerBuilder)
        {
            containerBuilder.Register<ILogProvider, NLogProvider>();
        }
    }
}
