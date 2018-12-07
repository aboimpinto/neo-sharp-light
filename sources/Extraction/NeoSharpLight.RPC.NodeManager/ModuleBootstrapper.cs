using System;
using NeoSharp.Core;
using NeoSharp.DependencyInjection;

namespace NeoSharpLight.RPC.NodeManager
{
    public class ModuleBootstrapper : IModuleBootstrapper, IApplicationEntryPoint
    {
        public void Start(IContainer container)
        {
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
