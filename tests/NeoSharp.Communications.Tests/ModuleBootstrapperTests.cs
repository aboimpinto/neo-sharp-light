using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeoSharp.DependencyInjection;
using NeoSharp.TestsHelper;

namespace NeoSharp.Communications.Tests
{
    [TestClass]
    public class ModuleBootstrapperTests : TesterBase
    {
        [TestMethod]
        public void Start_CorrectRegistrations()
        {
            // Arrange
            var containetMock = this.AutoMockContainer.GetMock<IContainer>();
            var testee = this.AutoMockContainer.Create<ModuleBootstrapper>();

            // Act
            testee.Start(containetMock.Object);

            // Asset
            containetMock.Verify(x => x.Register<INodeConnector, NodeConnector>(), Times.Once);
        }
    }
}
