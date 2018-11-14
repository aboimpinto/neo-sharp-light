using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using NeoSharp.Communications;
using NeoSharp.Core;
using NeoSharp.Core.Extensions;
using NeoSharp.DependencyInjection;
using NeoSharp.DependencyInjection.Unity;

namespace NeoSharp.Application
{
        public class Bootstrap : IDisposable
    {
        private readonly IConfigurationRoot neoSharpConfiguration;
        private readonly ModuleConfiguration moduleConfiguration;
        private readonly IContainer dependencyInjectionContainer;

        private bool disposed = false;

        public Bootstrap()
        {
            this.neoSharpConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            this.moduleConfiguration = this.neoSharpConfiguration.LoadConfiguration<ModuleConfiguration>();

            this.dependencyInjectionContainer = new Container();
        }

        /// <summary>
        /// Start bootstrapping.
        /// </summary>
        public void Start()
        {
            this.LoadModules();

            var nepSharpContext = this.dependencyInjectionContainer.Resolve<INeoSharpContext>();
            nepSharpContext.ApplicationConfiguration = this.neoSharpConfiguration;

            var nodeConnector = this.dependencyInjectionContainer.Resolve<INodeConnector>();
            nodeConnector.Connect();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
            }

            this.disposed = true;
        }

        private void LoadModules()
        {
            foreach (var module in this.moduleConfiguration.Modules)
            {
                var isLoaded = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Any(x => x.GetName().Name == module);

                Assembly assembly = null;
                if (isLoaded)
                {
                    assembly = AppDomain.CurrentDomain
                        .GetAssemblies()
                        .Single(x => x.GetName().Name == module);
                }
                else
                {
                    var moduleDll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{module}.dll");
                    assembly = Assembly.LoadFile(moduleDll);
                }

                var moduleBootstrappers = assembly.DefinedTypes
                    .Where(x => x.ImplementedInterfaces.Contains(typeof(IModuleBootstrapper)));

                if (moduleBootstrappers.Any())
                {
                    // Each module should only contain one IModuleBootstrapper implementation.
                    var moduleBootstrapInstance = assembly.CreateInstance(moduleBootstrappers.Single().FullName) as IModuleBootstrapper;
                    moduleBootstrapInstance.Start(this.dependencyInjectionContainer);
                }
            }

            var xx = this.dependencyInjectionContainer.Resolve<NeoSharp.Serialization.IBinarySerializer>();
        }
    }
}