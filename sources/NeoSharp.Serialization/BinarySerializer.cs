using System.IO;
using System.Text;

namespace NeoSharp.Serialization
{
    public class BinarySerializer : IBinarySerializer
    {
        public byte[] Serialize<TObject>(TObject obj)
        {
            var properties = obj.GetType().GetProperties();

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream, Encoding.UTF8, true))
                {
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(uint))
                        {
                            writer.Write((uint)property.GetValue(obj));
                        }
                    }

                    writer.Flush();
                }

                return memoryStream.ToArray();
            }
        }
    }
}