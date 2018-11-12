using System;

namespace NeoSharp.Serialization
{
    public interface IBinarySerializer
    {
        byte[] Serialize<TObject>(TObject obj);
    }
}
