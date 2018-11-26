using System;
using System.Collections.Generic;

namespace NeoSharp.DependencyInjection
{
    public interface IContainer
    {
        object Resolve(Type serviceType);

        TEntity Resolve<TEntity>()
            where TEntity : class;

        IEnumerable<TEntity> ResolveAll<TEntity>()
            where TEntity : class;

        TEntity Resolve<TEntity>(string instanceName)
            where TEntity : class;

        void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        void Register<TService>(TService implementation)
            where TService : class;

        void Register<TService, TImplementation>(string instanceName)
            where TService : class
            where TImplementation : class, TService;
        
        void Register(Type service, Type implementation);

        void Register(Type service, Type implementation, string instanceName);

        void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        void RegisterTypes(IEnumerable<Type> collectionOfTypes);
    }
}