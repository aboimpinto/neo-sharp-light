using NeoSharp.DependencyInjection;

namespace NeoSharp.Core
{
    public interface IApplicationEntryPoint
    {
        void StartApplication(IContainer container, string[] args);
    }
}
