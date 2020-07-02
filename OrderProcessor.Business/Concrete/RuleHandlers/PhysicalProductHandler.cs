using OrderProcessor.Business.Interfaces;
using OrderProcessor.DTO;
using OrderProcessor.DTO.Common;

namespace OrderProcessor.Business.Concrete
{
    public class PhysicalProductHandler : IRuleHandler
    {
        private readonly IShipmentProvider _shipmentProvider;
        public PhysicalProductHandler(IShipmentProvider shipmentProvider)
        {
            _shipmentProvider = shipmentProvider;
        }
        /// <summary>
        /// This handler will execute only for Physical product type is brought
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public bool IsHandle(OrderItem orderItem)
        {
            return orderItem.Product == Product.PhysicalProduct;
        }

        /// <summary>
        /// Physical Product Handler
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public OrderItem ProcessOrderItem(Order order, OrderItem orderItem)
        {
            //Generate shipping slip for physical product
            var generateShipmentSlip = $"{_shipmentProvider.GeneratePackagingShip(order.Customer)} for shipment";
            //set the step
            orderItem.OrderProcessSteps = string.IsNullOrEmpty(orderItem.OrderProcessSteps) ? generateShipmentSlip : $",{generateShipmentSlip}";
            return orderItem;
        }
    }
}
