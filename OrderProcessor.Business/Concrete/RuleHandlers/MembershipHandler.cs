using OrderProcessor.Business.Interfaces;
using OrderProcessor.DTO;
using OrderProcessor.DTO.Common;

namespace OrderProcessor.Business.Concrete.RuleHandlers
{
    public class MembershipHandler : IRuleHandler
    {
        private readonly INotificationProvider _notificationProvider;
        public MembershipHandler(INotificationProvider notificationProvider)
        {
            _notificationProvider = notificationProvider;
        }

        /// <summary>
        /// This handler will execute only for new membership activation product is brought
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public bool IsHandle(OrderItem orderItem)
        {
            return orderItem.Product == Product.ActivateMembership;
        }

        /// <summary>
        /// MemberShip Handler
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public OrderItem ProcessOrderItem(Order order, OrderItem orderItem)
        {
            //Activate the requested membership 
            var newActivemembership = $"Activate Membership for SkuId - {orderItem.SKUId}";
            //set the step
            orderItem.OrderProcessSteps = string.IsNullOrEmpty(orderItem.OrderProcessSteps) ? newActivemembership : $"{orderItem.OrderProcessSteps},{newActivemembership}";
            //We can use json string for sending msg in notification. We need to send email to customer with upgrade details
            var notification = _notificationProvider.SendNotification(order, $"Activate new membership with skuId - {orderItem.SKUId} and details");
            //set the step
            orderItem.OrderProcessSteps = $"{orderItem.OrderProcessSteps},{notification}";
            return orderItem;
        }
    }
}
