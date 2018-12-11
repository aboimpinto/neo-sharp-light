using System;
using NeoSharp.Core;
using NeoSharp.DependencyInjection;
using NeoSharpLight.RPC.NodeManager.RedisDumpDb;
using NeoSharpLight.RPC.NodeManager.RpcNodeAccess;

namespace NeoSharpLight.RPC.NodeManager
{
    public class ModuleBootstrapper : IModuleBootstrapper, IApplicationEntryPoint
    {
        public void Start(IContainer container)
        {
            container.RegisterSingleton<IDbAccess, DbAccess>();
            container.RegisterSingleton<INodeAccess, NodeAccess>();

            container.Register<IExtractionManager, ExtractionManager>();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void StartApplication(IContainer container)
        {
            var extractionManager = container.Resolve<IExtractionManager>();
            extractionManager.StartExtraction();
        }
    }
}
