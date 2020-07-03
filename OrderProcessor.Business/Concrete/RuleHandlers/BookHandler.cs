using OrderProcessor.Business.Interfaces;
using OrderProcessor.DTO;
using OrderProcessor.DTO.Common;

namespace OrderProcessor.Business.Concrete
{
    public class BookHandler : IRuleHandler
    {
        private readonly IShipmentProvider _shipmentProvider;
        public BookHandler(IShipmentProvider shipmentProvider)
        {
            _shipmentProvider = shipmentProvider;
        }
        /// <summary>
        /// This handler will execute only when Book is brought
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public bool IsHandle(OrderItem orderItem)
        {
            return orderItem.Product == Product.Book;
        }

        /// <summary>
        /// Book handler 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public OrderItem ProcessOrderItem(Order order, OrderItem orderItem)
        {
            //Generate shipping slip for physical product
            var generateShipmentSlip = $"{_shipmentProvider.GeneratePackagingShip(order.Customer)} for shipment";
            //set the step
            orderItem.OrderProcessSteps = string.IsNullOrEmpty(orderItem.OrderProcessSteps) ? generateShipmentSlip : $"{orderItem.OrderProcessSteps},{generateShipmentSlip}";

            //Generate duplicate shipping slip for royalty department
            var generateDuplicateShipmentSlip = $"{_shipmentProvider.GeneratePackagingShip(order.Customer)} for shipment for royalty department";
            //set the step
            orderItem.OrderProcessSteps = $"{orderItem.OrderProcessSteps},{generateDuplicateShipmentSlip}";
            return orderItem;
        }
    }
}
