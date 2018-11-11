namespace NeoSharp.Cryptography
{
    public interface ICrypto
    {
         byte[] Sha256(byte[] message, int offset, int count);

         byte[] Sha256(byte[] message);

         uint Checksum(byte[] byteArray);
    }
}