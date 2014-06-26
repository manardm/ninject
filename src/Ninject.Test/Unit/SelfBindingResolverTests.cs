#if !NO_MOQ
namespace Ninject.Tests.Unit.SelfBindingResolverTests
{
    using Moq;
    using Ninject.Activation;
    using Ninject.Activation.Blocks;
    using Ninject.Infrastructure;
    using Ninject.Parameters;
    using Ninject.Planning.Bindings;
    using Ninject.Planning.Bindings.Resolvers;
    using Ninject.Syntax;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class SelfBindingResolverContext
    {
        protected Mock<IContext> contextMock;

        public SelfBindingResolverContext()
        {
            this.SetUp();
        }

        public void SetUp()
        {
            this.contextMock = new Mock<IContext>();
        }
    }

    public class WhenResolveIsCalled : SelfBindingResolverContext
    {
        [Fact]
        public void ReturnsEmptyEnumerableIfServiceHasNoPublicConstructor()
        {
            var kernel = new StandardKernel();
            var serviceToResolve = typeof(DummyService);
            var selfBindingResolver = new SelfBindingResolver();

            var result = selfBindingResolver.Resolve(
                new Multimap<System.Type, IBinding>(),
                kernel.CreateRequest(
                    serviceToResolve,
                    null,
                    Enumerable.Empty<IParameter>(),
                    false,
                    true));

            Assert.Empty(result);
        }
    }

    public interface IDummyDependency
    {
        void DoWork();
    }

    public class DummyService
    {
        private IKernel _kernel;
        private IDummyDependency _dummyDependency;

        private DummyService(IKernel kernel, IDummyDependency dummyDependency)
        {
            _kernel = kernel;
            _dummyDependency = dummyDependency;
        }

        public static DummyService Construct(IDummyDependency dummyDependency)
        {
            return new DummyService(new Ninject.StandardKernel(), dummyDependency);
        }
    }
}
#endif