using Moq;
using OrderProcessor.Business.Concrete.RuleHandlers;
using OrderProcessor.Business.Interfaces;
using OrderProcessor.Business.Tests.TestData;
using OrderProcessor.DTO;
using System.Linq;
using Xunit;

namespace OrderProcessor.Business.Tests.Concrete.RuleHandlers
{
    public class MembershipHandlerTests
    {
        private Mock<INotificationProvider> _mockNotificationProvider;

        public MembershipHandlerTests()
        {
            _mockNotificationProvider = new Mock<INotificationProvider>();
        }

        [Fact]
        public void IsHandleTrue()
        {
            var unitUnderTest = new MembershipHandler(_mockNotificationProvider.Object);
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.True(result);
        }

        [Fact]
        public void IsHandleFalse()
        {
            var unitUnderTest = new MembershipHandler(_mockNotificationProvider.Object);
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.False(result);
        }

        [Fact]
        public void ProcessOrderItemIsSuccessActivateMembership()
        {
            _mockNotificationProvider.Setup(notification => notification.SendNotification(It.IsAny<Order>(), It.IsAny<string>()))
               .Returns($"Send email to {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().Customer.Email} for Activate new membership with skuId - {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault().SKUId} and details");
            var unitUnderTest = new MembershipHandler(_mockNotificationProvider.Object);
            var result = unitUnderTest.ProcessOrderItem(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault(),
                OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault());
            var steps = result.OrderProcessSteps.Split(",");
            Assert.NotNull(result.OrderProcessSteps);
            Assert.Contains($"Activate Membership for SkuId - {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault().SKUId}", steps[0]);
        }

        [Fact]
        public void ProcessOrderItemIsSuccessNotification()
        {
            _mockNotificationProvider.Setup(notification => notification.SendNotification(It.IsAny<Order>(), It.IsAny<string>()))
               .Returns($"Send email to {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().Customer.Email} for Activate new membership with skuId - {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault().SKUId} and details");
            var unitUnderTest = new MembershipHandler(_mockNotificationProvider.Object);
            var result = unitUnderTest.ProcessOrderItem(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault(),
                OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault());
            var steps = result.OrderProcessSteps.Split(",");
            Assert.NotNull(result.OrderProcessSteps);
            Assert.Contains($"Activate new membership with skuId - {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault().SKUId} and details", steps[1]);
        }
    }
}
