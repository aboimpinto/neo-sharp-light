using System;
using System.IO;
using System.Text;
using NeoSharp.Serialization.ExtensionMethods;

namespace NeoSharp.Serialization.CustomSerializers
{
    public class StringCustomSerializer : ICustomSerializer
    {
        public bool CanHandle(Type typeToHandle)
        {
            return typeof(string) == typeToHandle;
        }

        public object Deserialize(BinaryReader binaryReader)
        {
            return binaryReader.ReadVarString();
        }

        public void Serialize(BinaryWriter binaryWriter, object value)
        {
            var data = Encoding.UTF8.GetBytes((string)value);
            binaryWriter.WriteVarBytes(data);
        }
    }
}
