using OrderProcessor.Business.Concrete;
using OrderProcessor.Business.Tests.TestData;
using System.Linq;
using Xunit;

namespace OrderProcessor.Business.Tests.Concrete.RuleHandlers
{
    public class CommissionProviderHandlerTests
    {

        [Fact]
        public void IsHandleTrueForPhysicalProduct()
        {
            var unitUnderTest = new CommissionProviderHandler();
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.PhysicalProduct).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.True(result);
        }
        [Fact]
        public void IsHandleTrueForBook()
        {
            var unitUnderTest = new CommissionProviderHandler();
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.Book).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.True(result);
        }

        [Fact]
        public void IsHandleFalse()
        {
            var unitUnderTest = new CommissionProviderHandler();
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.False(result);
        }

        [Fact]
        public void ProcessOrderItemIsSuccess()
        {
            var unitUnderTest = new CommissionProviderHandler();
            var result = unitUnderTest.ProcessOrderItem(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.PhysicalProduct).FirstOrDefault(),
                OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.PhysicalProduct).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.NotNull(result.OrderProcessSteps);
            Assert.Contains($"Raise payment for the Agent {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault().AgentId}", result.OrderProcessSteps);
        }
    }
}
