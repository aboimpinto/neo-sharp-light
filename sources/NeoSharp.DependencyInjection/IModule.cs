using System;

namespace NeoSharp.DependencyInjection
{
    public interface IModule
    {
        void Register(IContainerModule containerModule);        
    }
}
