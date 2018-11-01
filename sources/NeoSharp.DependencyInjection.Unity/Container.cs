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

        public TEntity Resolve<TEntity>(string instanceName)
            where TEntity : class
        {
            return this.container.Resolve<TEntity>(instanceName);
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this.container.RegisterType<TService, TImplementation>();
        }

        public void Register<TService>(TService implementation)
            where TService : class
        {
            this.container.RegisterInstance<TService>(implementation);   
        }

        public void Register<TService, TImplementation>(string instanceName)
            where TService : class
            where TImplementation : class, TService
        {
            this.container.RegisterType<TService, TImplementation>(instanceName);
        }

        public void Register(Type service, Type implementation)
        {
            this.container.RegisterType(service, implementation);
        }

        public void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this.container.RegisterType<TService, TImplementation>(new ContainerControlledLifetimeManager());
        }
    }
}