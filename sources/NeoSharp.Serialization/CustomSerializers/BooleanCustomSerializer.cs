using System;
using System.IO;

namespace NeoSharp.Serialization.CustomSerializers
{
    public class BooleanCustomSerializer : ICustomSerializer
    {
        private const byte BTRUE = 0x01;
        private const byte BFALSE = 0x00;

        public bool CanHandle(Type typeToHandle)
        {
            return typeof(bool) == typeToHandle;
        }

        public object Deserialize(BinaryReader binaryReader)
        {
            return binaryReader.ReadByte() != BFALSE;
        }

        public void Serialize(BinaryWriter binaryWriter, object value)
        {
            if ((bool)value)
            {
                binaryWriter.Write(BTRUE);
            }
            else
            {
                binaryWriter.Write(BFALSE);
            }
        }
    }
}
