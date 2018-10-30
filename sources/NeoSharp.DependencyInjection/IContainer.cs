using System;

namespace NeoSharp.DependencyInjection
{
    public interface IContainer
    {
        object Resolve(Type serviceType);

        TEntity Resolve<TEntity>()
            where TEntity : class;

        void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;
        
        void Register(Type service, Type implementation);

        void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;
    }
}