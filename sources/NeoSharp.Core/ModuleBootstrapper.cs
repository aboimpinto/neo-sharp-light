using NeoSharp.DependencyInjection;

namespace NeoSharp.Core
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.RegisterSingleton<INeoSharpContext, NeoSharpContext>();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}