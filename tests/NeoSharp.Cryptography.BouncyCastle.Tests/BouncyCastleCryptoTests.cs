using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoSharp.Core.Extensions;
using NeoSharp.TestsHelper;

namespace NeoSharp.Cryptography.BouncyCastle.Tests
{
    [TestClass]
    public class BouncyCastleCryptoTests : TesterBase
    {
        [TestMethod]
        public void Sha256_ReturnRightSha256()
        {
            // Arrange
            var data = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
            var testee = this.AutoMockContainer.Create<BouncyCastleCrypto>();

            // Act 
            var result = testee.Sha256(data);

            // Assert
            result.ToHexString()
                .Should()
                .Be("be45cb2605bf36bebde684841a28f0fd43c69850a3dce5fedba69928ee3a8991");
        }
    }
}
