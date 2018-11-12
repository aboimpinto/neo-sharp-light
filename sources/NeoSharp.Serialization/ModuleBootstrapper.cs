using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharp.Serialization
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.Register<IBinarySerializer, BinarySerializer>();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}