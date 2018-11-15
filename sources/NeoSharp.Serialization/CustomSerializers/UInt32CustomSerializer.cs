using System;
using System.IO;

namespace NeoSharp.Serialization.CustomSerializers
{
    public class UInt32CustomSerializer : ICustomSerializer
    {
        public bool CanHandle(Type typeToHandle)
        {
            return typeof(uint) == typeToHandle;
        }

        public object Deserialize(BinaryReader binaryReader)
        {
            return binaryReader.ReadInt32();
        }

        public void Serialize(BinaryWriter binaryWriter, object value)
        {
            binaryWriter.Write((uint)value);
        }
    }
}
