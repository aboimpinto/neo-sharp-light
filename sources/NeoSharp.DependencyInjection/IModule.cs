using System;

namespace NeoSharp.DependencyInjection
{
    public interface IModule
    {
        void Register(IContainerBuilder containerBuilder);        
    }
}
