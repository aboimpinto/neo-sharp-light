using System;
using System.IO;

namespace NeoSharp.Serialization.CustomSerializers
{
    public interface ICustomSerializer
    {
        bool CanHandle(Type typeToHandle);

        void Serialize(BinaryWriter binaryWriter, object value);

        object Deserialize(BinaryReader binaryReader);
    }
}
