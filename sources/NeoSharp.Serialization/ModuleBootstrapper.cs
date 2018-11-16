using System.Linq;
using NeoSharp.Core;
using NeoSharp.DependencyInjection;
using NeoSharp.Serialization.CustomSerializers;

namespace NeoSharp.Serialization
{
    public class ModuleBootstrapper : IModuleBootstrapper
    {
        public void Start(IContainer container)
        {
            container.Register<ICustomSerializer, UInt16CustomSerializer>(typeof(UInt16CustomSerializer).Name);
            container.Register<ICustomSerializer, UInt32CustomSerializer>(typeof(UInt32CustomSerializer).Name);
            container.Register<ICustomSerializer, UInt64CustomSerializer>(typeof(UInt64CustomSerializer).Name);
            container.Register<ICustomSerializer, StringCustomSerializer>(typeof(StringCustomSerializer).Name);
            container.Register<ICustomSerializer, BooleanCustomSerializer>(typeof(BooleanCustomSerializer).Name);

            var typesOfCustomSerializers = typeof(ICustomSerializer)
                .Assembly
                .GetTypes()
                .Where(x => x.IsClass)
                .Where(x => typeof(ICustomSerializer).IsAssignableFrom(x))
                .Select(x => x.UnderlyingSystemType);
            container.RegisterTypes(typesOfCustomSerializers);

            container.Register<IBinarySerializer, BinarySerializer>();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}