using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharp.Cryptography.BouncyCastle
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.Register<ICrypto, BouncyCastleCrypto>();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}