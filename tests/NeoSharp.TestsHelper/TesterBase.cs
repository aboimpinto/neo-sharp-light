using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeoSharp.TestsHelper.AutoMock;

namespace NeoSharp.TestsHelper
{
    public abstract class TesterBase
    {
        private readonly MockRepository mockRepository;
        private readonly Random rand;

        private const string RandStringAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly int RandStringAlphabetLength = RandStringAlphabet.Length;

        public IAutoMockContainer AutoMockContainer { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected TesterBase()
        {
            this.rand = new Random(Environment.TickCount);
            this.mockRepository = new MockRepository(MockBehavior.Loose);
            AutoMockContainer = new UnityAutoMockContainer(mockRepository);
        }

        /// <summary>
        /// Generate random strings
        /// </summary>
        /// <param name="length">String lenght</param>
        /// <returns>String</returns>
        public string RandomString(int length)
        {
            var result = new byte[length];
            this.rand.NextBytes(result);

            for (int i = 0; i < length; ++i)
            {
                result[i] = (byte)RandStringAlphabet[result[i] % RandStringAlphabetLength];
            }

            return Encoding.ASCII.GetString(result);
        }

        /// <summary>
        /// Generate a random integer
        /// </summary>
        /// <returns>A positive integer</returns>
        public int RandomInt()
        {
            return this.rand.Next();
        }

        /// <summary>
        /// Generate a random integer with a max value
        /// </summary>
        /// <param name="max"></param>
        /// <returns>A positive integer that is smaller than max</returns>
        public int RandomInt(int max)
        {
            return this.rand.Next(max);
        }

        public int RandomInt(int minValue, int maxValue)
        {
            return this.rand.Next(minValue, maxValue);
        }

        public byte[] RandomByteArray(int len)
        {
            var output = new byte[len];
            this.rand.NextBytes(output);
            return output;
        }

        /// <summary>
        /// Verify All
        /// </summary>
        public void VerifyAll()
        {
            this.mockRepository.VerifyAll();
        }
    }
}
