using OrderProcessor.Business.Concrete;
using OrderProcessor.Business.Tests.TestData;
using OrderProcessor.DTO.Common;
using System.Linq;
using Xunit;

namespace OrderProcessor.Business.Tests
{
    public class OrderProcessorBusinessTests
    {
        [Fact]
        public void SubmitOrderTestSuccessWithOrderStatusAsCompleted()
        {
            var unitUnderTest = new OrderProcessorBusiness();
            var order = unitUnderTest.SubmitOrder(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.PhysicalProduct).FirstOrDefault());
            Assert.True(order.OrderStatus == OrderStatus.Completed);
        }
        [Fact]
        public void SubmitOrderTestSuccessWithOrderValidStepsForPhysicalProduct()
        {
            var unitUnderTest = new OrderProcessorBusiness();
            var order = unitUnderTest.SubmitOrder(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.PhysicalProduct).FirstOrDefault());
            var steps = order.OrderItems.FirstOrDefault().OrderProcessSteps.Split(",");
            Assert.True(steps.Length == 2);
            Assert.Contains("Raise payment for the Agent 10001", steps[0]);
            Assert.Contains("Generate slip for Abhijit for shipment", steps[1]);
            
        }
        
        [Fact]
        public void SubmitOrderTestSuccessWithOrderValidStepsForBook()
        {
            var unitUnderTest = new OrderProcessorBusiness();
            var order = unitUnderTest.SubmitOrder(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.Book).FirstOrDefault());
            var steps = order.OrderItems.FirstOrDefault().OrderProcessSteps.Split(",");
            Assert.True(steps.Length == 3);
            Assert.Contains("Raise payment for the Agent 10001", steps[0]);
            Assert.Contains("Generate slip for Abhijit for shipment", steps[1]);
            Assert.Contains("Generate slip for Abhijit for shipment for royalty department", steps[2]);

        }

        [Fact]
        public void SubmitOrderTestSuccessWithOrderValidStepsForActivateMembership()
        {
            var unitUnderTest = new OrderProcessorBusiness();
            var order = unitUnderTest.SubmitOrder(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault());
            var steps = order.OrderItems.FirstOrDefault().OrderProcessSteps.Split(",");
            Assert.True(steps.Length == 2);
            Assert.Contains("Activate Membership for SkuId - 100123", steps[0]);
            Assert.Contains("Send email to abc@email.com for Activate new membership with skuId - 100123 and details", steps[1]);

        }

        [Fact]
        public void SubmitOrderTestSuccessWithOrderValidStepsForUpgradeMembership()
        {
            var unitUnderTest = new OrderProcessorBusiness();
            var order = unitUnderTest.SubmitOrder(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault());
            var steps = order.OrderItems.FirstOrDefault().OrderProcessSteps.Split(",");
            Assert.True(steps.Length == 3);
            Assert.Contains("Upgrade Membership to 100123 for customerId 1", steps[0]);
            Assert.Contains("Activate Upgraded Membership - 100123 for customerId 1", steps[1]);
            Assert.Contains("Send email to abc@email.com for upgrade membership with skuId - 100123 and details", steps[2]);

        }

        [Fact]
        public void SubmitOrderTestSuccessWithOrderValidStepsForSkiVideo()
        {
            var unitUnderTest = new OrderProcessorBusiness();
            var order = unitUnderTest.SubmitOrder(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault());
            var steps = order.OrderItems.FirstOrDefault().OrderProcessSteps.Split(",");
            Assert.True(steps.Length == 2);
            Assert.Contains("Adding free first Aid video", steps[0]);
            Assert.Contains("Generate slip for Abhijit for shipment of Learning to Ski with free first Aid video", steps[1]);

        }
    }
}
