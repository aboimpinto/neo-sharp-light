using System;
using System.IO;

namespace NeoSharp.Serialization.CustomSerializers
{
    public class UInt16CustomSerializer : ICustomSerializer
    {
        public bool CanHandle(Type typeToHandle)
        {
            return typeof(ushort) == typeToHandle;
        }

        public object Deserialize(BinaryReader binaryReader)
        {
            return binaryReader.ReadUInt16();
        }

        public void Serialize(BinaryWriter binaryWriter, object value)
        {
            binaryWriter.Write((ushort)value);
        }
    }
}
