using System;
using System.Linq;

namespace NeoSharp.Communications.Extensions
{
    public static class TypeExtensions
    {
        public static bool ImplementsGeneric(this Type type, Type genericType)
        {
            var implements = type
                .GetInterfaces()
                .Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == genericType);

            return implements;
        }
    }
}
