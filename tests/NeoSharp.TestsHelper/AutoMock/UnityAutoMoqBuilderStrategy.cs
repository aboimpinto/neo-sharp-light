using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using Unity.Builder;
using Unity.Builder.Strategy;

namespace NeoSharp.TestsHelper.AutoMock
{
    public class UnityAutoMoqBuilderStrategy : BuilderStrategy
    {
        private readonly UnityAutoMockContainer autoMockContainer;
        private readonly MethodInfo createMethod;
        private readonly MockRepository mockRepository;
        private readonly Dictionary<Type, object> mocks;

        public UnityAutoMoqBuilderStrategy(
            MockRepository mockRepository,
            UnityAutoMockContainer autoMockContainer)
        {
            this.mockRepository = mockRepository;
            this.autoMockContainer = autoMockContainer;

            this.createMethod = mockRepository.GetType().GetMethod("Create", new Type[] { });

            this.mocks = new Dictionary<Type, object>();
        }

        public override void PreBuildUp(IBuilderContext context)
        {
            var type = context.OriginalBuildKey.Type;

            if (type.IsInterface || type.IsAbstract)
            {
                context.Existing = GetOrCreateMock(type);
                context.BuildComplete = true;
            }
        }

        private object GetOrCreateMock(Type t)
        {
            if (this.mocks.ContainsKey(t))
            {
                return this.mocks[t];
            }

            var genericType = typeof(Mock<>).MakeGenericType(t);

            var specificCreateMethod = this.createMethod.MakeGenericMethod(t);
            var mock = (Mock)specificCreateMethod.Invoke(mockRepository, null);

            var interfaceImplementations = this.autoMockContainer.GetInterfaceImplementations(t);
            if (interfaceImplementations != null)
            {
                foreach (var implementation in interfaceImplementations.GetImplementations())
                {
                    genericType.GetMethod("As").MakeGenericMethod(implementation).Invoke(mock, null);
                }
            }

            var mockedInstance = mock.Object;
            this.mocks.Add(t, mockedInstance);

            return mockedInstance;
        }
    }
}
