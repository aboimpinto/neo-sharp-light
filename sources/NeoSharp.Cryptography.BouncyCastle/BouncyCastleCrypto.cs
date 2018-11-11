using System;
using NeoSharp.Core.Extensions;
using Org.BouncyCastle.Crypto.Digests;

namespace NeoSharp.Cryptography.BouncyCastle
{
    public class BouncyCastleCrypto : ICrypto
    {
        /// <inheritdoc />
        public byte[] Sha256(byte[] message)
        {
            return this.Sha256(message, 0, message.Length);
        }

        /// <inheritdoc />
        public byte[] Sha256(byte[] message, int offset, int count)
        {
            var hash = new Sha256Digest();
            hash.BlockUpdate(message, offset, count);

            var result = new byte[32];
            hash.DoFinal(result, 0);

            return result;
        }

        /// <inheritdoc />
        public uint Checksum(byte[] byteArray)
        {
            return this.Sha256(this.Sha256(byteArray, 0, byteArray.Length), 0, 32).ToUInt32();
        }
    }
}
