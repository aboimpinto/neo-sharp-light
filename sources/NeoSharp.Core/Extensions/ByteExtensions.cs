using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSharp.Core.Extensions
{
    public static class ByteExtensions
    {
        public static string ToHexString(this IEnumerable<byte> value, bool append0X = false)
        {
            var sb = new StringBuilder();

            foreach (var b in value)
            {
                sb.AppendFormat("{0:x2}", b);
            }

            if (append0X && sb.Length > 0)
            {
                return $"0x{sb}";
            }

            return sb.ToString();
        }

        public static uint ToUInt32(this byte[] value, int startIndex = 0)
        {
            return BitConverter.ToUInt32(value, startIndex);
        }
    }
}