using Moq;
using OrderProcessor.Business.Concrete;
using OrderProcessor.Business.Interfaces;
using OrderProcessor.Business.Tests.TestData;
using OrderProcessor.DTO;
using System.Linq;
using Xunit;

namespace OrderProcessor.Business.Tests.Concrete.RuleHandlers
{
    public class BookHandlerTests
    {
        private Mock<IShipmentProvider> _mockShipmentProvider;
        public BookHandlerTests()
        {
            _mockShipmentProvider = new Mock<IShipmentProvider>();
        }
        [Fact]
        public void IsHandleTrue()
        {
            var unitUnderTest = new BookHandler(_mockShipmentProvider.Object);
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.Book).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.True(result);
        }

        [Fact]
        public void IsHandleFalse()
        {
            var unitUnderTest = new BookHandler(_mockShipmentProvider.Object);
            var result = unitUnderTest.IsHandle(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.SkiVideo).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.False(result);
        }

        [Fact]
        public void ProcessOrderItemIsSuccess()
        {
            _mockShipmentProvider.Setup(shipment => shipment.GeneratePackagingShip(It.IsAny<Customer>()))
               .Returns($"Generate slip for {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.Book).FirstOrDefault().Customer.CustomerName}");
            var unitUnderTest = new BookHandler(_mockShipmentProvider.Object);
            var result = unitUnderTest.ProcessOrderItem(OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.Book).FirstOrDefault(), 
                OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.Book).FirstOrDefault().OrderItems.FirstOrDefault());
            Assert.NotNull(result.OrderProcessSteps);
            Assert.Contains($"Generate slip for {OrdersRepositoryTestData.OneItemOrderPassProductType(DTO.Common.Product.Book).FirstOrDefault().Customer.CustomerName} for shipment", result.OrderProcessSteps);
        }
    }
}
