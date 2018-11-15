using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NeoSharp.Logging;
using NeoSharp.Serialization.CustomSerializers;

namespace NeoSharp.Serialization
{
    public class BinarySerializer : IBinarySerializer
    {
        private readonly IEnumerable<ICustomSerializer> customSerializers;
        private readonly ILogger<BinarySerializer> logger;

        public BinarySerializer(
            ICustomSerializer[] customSerializers,
            ILogger<BinarySerializer> logger)
        {
            this.customSerializers = customSerializers;
            this.logger = logger;
        }

        public byte[] Serialize<TObject>(TObject obj)
        {
            var properties = obj.GetType().GetProperties();

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream, Encoding.UTF8, true))
                {
                    foreach (var property in properties)
                    {
                        try
                        {
                            var customSerializer = this.customSerializers
                                                .Single(x => x.CanHandle(property.PropertyType));

                            customSerializer.Serialize(writer, property.GetValue(obj));
                        }
                        catch (Exception ex)
                        {
                            this.logger.LogError(ex, $"Property {property.Name} with type {property.PropertyType} cannot be serialized because missing custom serializer.");
                        }
                    }

                    writer.Flush();
                }

                return memoryStream.ToArray();
            }
        }
    }
}