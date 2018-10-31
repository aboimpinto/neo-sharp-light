using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharp.Communications
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.Register<INodeConnector, NodeConnector>();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}