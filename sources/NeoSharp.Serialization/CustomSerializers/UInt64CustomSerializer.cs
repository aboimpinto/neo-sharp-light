using System;
using System.IO;

namespace NeoSharp.Serialization.CustomSerializers
{
    public class UInt64CustomSerializer : ICustomSerializer
    {
        public bool CanHandle(Type typeToHandle)
        {
            return typeof(ulong) == typeToHandle;
        }

        public object Deserialize(BinaryReader binaryReader)
        {
            return binaryReader.ReadUInt64();
        }

        public void Serialize(BinaryWriter binaryWriter, object value)
        {
            binaryWriter.Write((ulong)value);
        }
    }
}
