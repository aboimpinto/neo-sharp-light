using System;
using Microsoft.Practices.Unity;

namespace NeoSharp.DependencyInjection.Unity
{
    public class Container : IContainer
    {
        private readonly UnityContainer container;

        public Container()
        {
            this.container = new UnityContainer();
        }

        public object Resolve(Type serviceType)
        {
            return this.container.Resolve(serviceType);
        }

        public TEntity Resolve<TEntity>()
            where TEntity : class
        {
            return this.container.Resolve<TEntity>();
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this.container.RegisterType<TService, TImplementation>();
        }

        public void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this.container.RegisterType<TService, TImplementation>(new ContainerControlledLifetimeManager());
        }
    }
}