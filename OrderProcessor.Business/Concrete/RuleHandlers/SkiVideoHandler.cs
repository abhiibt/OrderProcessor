using OrderProcessor.Business.Interfaces;
using OrderProcessor.DTO.Common;
using OrderProcessor.DTO;

namespace OrderProcessor.Business.Concrete
{
    public class SkiVideoHandler : IRuleHandler
    {
        private readonly IShipmentProvider _shipmentProvider;
        public SkiVideoHandler(IShipmentProvider shipmentProvider)
        {
            _shipmentProvider = shipmentProvider;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public bool IsHandle(OrderItem orderItem)
        {
            return orderItem.Product == Product.SkiVideo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public OrderItem ProcessOrderItem(Order order, OrderItem orderItem)
        {
            //Generate shipping slip for physical product
            var addingFreeFirstAidVideo = $"Adding free first Aid video";
            //set the step
            orderItem.OrderProcessSteps = string.IsNullOrEmpty(orderItem.OrderProcessSteps) ? addingFreeFirstAidVideo : $"{orderItem.OrderProcessSteps},{addingFreeFirstAidVideo}";
            order.OrderItems.Add(new OrderItem()
            {
                OrderItemId = order.OrderItems.Count + 1,
                OrderProcessSteps = "Adding free first aid video to package",
                Product = Product.Video,
                SKUId = (int)Product.Video,
                Price = 0
            });

            //Generate shipping slip for physical product
            var generateShipmentSlip = $"{_shipmentProvider.GeneratePackagingShip(order.Customer)} for shipment of Learning to Ski with free first Aid video";
            //set the step
            orderItem.OrderProcessSteps = $"{orderItem.OrderProcessSteps},{generateShipmentSlip}";
            return orderItem;
        }
    }
}
