using Moq;
using OrderProcessor.Business.Concrete;
using OrderProcessor.Business.Interfaces;
using OrderProcessor.Business.Tests.TestData;
using OrderProcessor.DTO;
using System.Linq;
using Xunit;

namespace OrderProcessor.Business.Tests.Concrete.RuleHandlers
{
    public class SkiVideoHandlerTests
    {
        private Mock<IShipmentProvider> _mockShipmentProvider;

        public SkiVideoHandlerTests()
        {
            _mockShipmentProvider = new Mock<IShipmentProvider>();
        }

        [Fact]
        public void IsHandleTrue()
        {
            var unitUnderTest = new SkiVideoHandler(_mockShipmentProvider.Object);
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.True(result);
        }

        [Fact]
        public void IsHandleFalse()
        {
            var unitUnderTest = new SkiVideoHandler(_mockShipmentProvider.Object);
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.PhysicalProduct).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.False(result);
        }

        [Fact]
        public void ProcessOrderItemIsSuccessForLearningToSkiAddFreeFirstAid()
        {
            var unitUnderTest = new SkiVideoHandler(_mockShipmentProvider.Object);
            var result = unitUnderTest.ProcessOrderItem(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault(),
                OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault().OrderItems.FirstOrDefault());
            var steps = result.OrderProcessSteps.Split(",");
            Assert.NotNull(result.OrderProcessSteps);
            Assert.Contains($"Adding free first Aid video", steps[0]);
        }

        [Fact]
        public void ProcessOrderItemIsSuccessForLearningToSki()
        {
            _mockShipmentProvider.Setup(shipment => shipment.GeneratePackagingShip(It.IsAny<Customer>()))
               .Returns($"Generate slip for {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault().Customer.CustomerName} for shipment of Learning to Ski with free first Aid video");
            var unitUnderTest = new SkiVideoHandler(_mockShipmentProvider.Object);
            var result = unitUnderTest.ProcessOrderItem(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault(),
                OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault().OrderItems.FirstOrDefault());
            var steps = result.OrderProcessSteps.Split(",");
            Assert.NotNull(result.OrderProcessSteps);
            Assert.Contains($"Generate slip for {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault().Customer.CustomerName} for shipment of Learning to Ski with free first Aid video", steps[1]);
        }        
    }
}
