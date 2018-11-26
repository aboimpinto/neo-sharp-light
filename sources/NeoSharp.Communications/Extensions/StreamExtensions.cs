using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NeoSharp.Communications.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> FillBufferAsync(this Stream stream, int size, CancellationToken cancellationToken)
        {
            const int MaxBufferSize = 4096;

            var buffer = new byte[Math.Min(size, MaxBufferSize)];

            using (var memory = new MemoryStream())
            {
                while (size > 0)
                {
                    var count = Math.Min(size, buffer.Length);

                    count = await stream
                        .ReadAsync(buffer, 0, count, cancellationToken)
                        .ConfigureAwait(false);

                    if (count <= 0)
                    {
                        throw new IOException();
                    }

                    memory.Write(buffer, 0, count);
                    size -= count;
                }

                return memory.ToArray();
            }
        }
    }
}
