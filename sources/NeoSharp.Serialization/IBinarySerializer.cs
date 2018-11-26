using System;
using System.IO;

namespace NeoSharp.Serialization
{
    public interface IBinarySerializer
    {
        byte[] Serialize<TObject>(TObject obj);

        TObject Deserialize<TObject>(BinaryReader reader);
    }
}
