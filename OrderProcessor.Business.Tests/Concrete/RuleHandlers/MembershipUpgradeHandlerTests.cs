using Moq;
using OrderProcessor.Business.Concrete.RuleHandlers;
using OrderProcessor.Business.Interfaces;
using OrderProcessor.Business.Tests.TestData;
using OrderProcessor.DTO;
using System.Linq;
using Xunit;

namespace OrderProcessor.Business.Tests.Concrete.RuleHandlers
{
    public class MembershipUpgradeHandlerTests
    {
        private Mock<INotificationProvider> _mockNotificationProvider;

        public MembershipUpgradeHandlerTests()
        {
            _mockNotificationProvider = new Mock<INotificationProvider>();
        }

        [Fact]
        public void IsHandleTrue()
        {
            var unitUnderTest = new MembershipUpgradeHandler(_mockNotificationProvider.Object);
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.True(result);
        }

        [Fact]
        public void IsHandleFalse()
        {
            var unitUnderTest = new MembershipUpgradeHandler(_mockNotificationProvider.Object);
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.False(result);
        }
        [Fact]
        public void ProcessOrderItemIsSuccessUpgradeMembership()
        {
            var unitUnderTest = new MembershipUpgradeHandler(_mockNotificationProvider.Object);
            var result = unitUnderTest.ProcessOrderItem(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault(),
                OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault());
            var steps = result.OrderProcessSteps.Split(",");
            Assert.NotNull(result.OrderProcessSteps);
            Assert.Contains($"Upgrade Membership to {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault().OrderItems.FirstOrDefault().SKUId} for customerId {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault().Customer.CustomerId}", steps[0]);
        }

        [Fact]
        public void ProcessOrderItemIsSuccessActivateMembership()
        {
            var unitUnderTest = new MembershipUpgradeHandler(_mockNotificationProvider.Object);
            var result = unitUnderTest.ProcessOrderItem(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault(),
                OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault());
            var steps = result.OrderProcessSteps.Split(",");
            Assert.NotNull(result.OrderProcessSteps);
            Assert.Contains($"Activate Upgraded Membership - {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault().OrderItems.FirstOrDefault().SKUId} for customerId {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault().Customer.CustomerId}", steps[1]);
        }

        [Fact]
        public void ProcessOrderItemIsSuccessNotification()
        {
            _mockNotificationProvider.Setup(notification => notification.SendNotification(It.IsAny<Order>(), It.IsAny<string>()))
               .Returns($"Send email to {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault().Customer.Email} for Activate new membership with skuId - {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault().SKUId} and details");
            var unitUnderTest = new MembershipUpgradeHandler(_mockNotificationProvider.Object);
            var result = unitUnderTest.ProcessOrderItem(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault(),
                OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.ActivateMembership).FirstOrDefault().OrderItems.FirstOrDefault());
            var steps = result.OrderProcessSteps.Split(",");
            Assert.NotNull(result.OrderProcessSteps);
            Assert.Contains($"Activate new membership with skuId - {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.UpgradeMembership).FirstOrDefault().OrderItems.FirstOrDefault().SKUId} and details", steps[2]);
        }
    }
}
