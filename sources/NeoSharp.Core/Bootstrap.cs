using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using NeoSharp.Core;
using NeoSharp.Core.Extensions;
using NeoSharp.DependencyInjection;
using NeoSharp.DependencyInjection.Unity;

namespace NeoSharpLight.Core
{
    public class Bootstrap
    {
        private readonly IConfigurationRoot neoSharpConfiguration;
        private readonly ModuleConfiguration moduleConfiguration;
        private readonly IContainer dependencyInjectionContainer;

        public Bootstrap()
        {
            this.neoSharpConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            this.moduleConfiguration = this.neoSharpConfiguration.LoadConfiguration<ModuleConfiguration>();

            this.dependencyInjectionContainer = new Container();
        }

        public void Start()
        {
            this.LoadModules();

            var nepSharpContext = this.dependencyInjectionContainer.Resolve<INeoSharpContext>();
            nepSharpContext.ApplicationConfiguration = this.neoSharpConfiguration;

            this.LoadApplicationEntryPoint();
        }

        private void LoadApplicationEntryPoint()
        {
            var applicationEntryPointConfiguration = this.neoSharpConfiguration.LoadConfiguration<ApplicationEntryPointConfiguration>();

            var assembly = this.LoadModuleIfNecessary(applicationEntryPointConfiguration.EntryPoint);

            var applicationEntryImplementation = assembly.DefinedTypes
                .SingleOrDefault(x => x.ImplementedInterfaces.Contains(typeof(IApplicationEntryPoint)));

            if (applicationEntryImplementation != null)
            {
                var applicationEntryPointInstance = assembly.CreateInstance(applicationEntryImplementation.FullName) as IApplicationEntryPoint;
                applicationEntryPointInstance.StartApplication(this.dependencyInjectionContainer);
            }
        }

        private void LoadModules()
        {
            foreach (var module in this.moduleConfiguration.Modules)
            {
                var assembly = this.LoadModuleIfNecessary(module);

                var moduleBootstrappers = assembly.DefinedTypes
                    .Where(x => x.ImplementedInterfaces.Contains(typeof(IModuleBootstrapper)));

                if (moduleBootstrappers.Any())
                {
                    // Each module should only contain one IModuleBootstrapper implementation.
                    var moduleBootstrapInstance = assembly.CreateInstance(moduleBootstrappers.Single().FullName) as IModuleBootstrapper;
                    moduleBootstrapInstance.Start(this.dependencyInjectionContainer);
                }
            }
        }

        private Assembly LoadModuleIfNecessary(string entryPointModule)
        {
            var isLoaded = AppDomain.CurrentDomain
                .GetAssemblies()
                .Any(x => x.GetName().Name == entryPointModule);

            Assembly assembly = null;
            if (isLoaded)
            {
                assembly = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Single(x => x.GetName().Name == entryPointModule);
            }
            else
            {
                var moduleDll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{entryPointModule}.dll");
                assembly = Assembly.LoadFile(moduleDll);
            }

            return assembly;
        }
    }
}
