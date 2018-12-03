using System;
using Microsoft.EntityFrameworkCore;
using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharp.DbAccess.MySQL
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.RegisterSingleton<DbContext, BlockchainContext>();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
