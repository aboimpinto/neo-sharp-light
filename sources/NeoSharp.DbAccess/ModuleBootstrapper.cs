using System;
using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharp.DbAccess
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.Register<IBlockchainDbAccess, BlockchainDbAccess>();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
