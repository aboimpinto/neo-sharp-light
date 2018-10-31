using Moq;
using Unity.Builder;
using Unity.Extension;

namespace NeoSharp.TestsHelper.AutoMock
{
    public class UnityAutoMoqExtension : UnityContainerExtension
    {
        private readonly MockRepository mockRepository;
        private readonly UnityAutoMockContainer autoMockContainer;

        public UnityAutoMoqExtension(
            MockRepository mockRepository,
            UnityAutoMockContainer autoMockContainer)
        {
            this.mockRepository = mockRepository;
            this.autoMockContainer = autoMockContainer;
        }

        protected override void Initialize()
        {
            Context.Strategies.Add(
                new UnityAutoMoqBuilderStrategy(mockRepository, autoMockContainer),
                UnityBuildStage.PreCreation);
        }
    }
}
