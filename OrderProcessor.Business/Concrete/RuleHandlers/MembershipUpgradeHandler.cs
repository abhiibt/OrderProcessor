using OrderProcessor.Business.Interfaces;
using OrderProcessor.DTO.Common;
using OrderProcessor.DTO;

namespace OrderProcessor.Business.Concrete.RuleHandlers
{
    public class MembershipUpgradeHandler : IRuleHandler
    {
        private readonly INotificationProvider _notificationProvider;
        public MembershipUpgradeHandler(INotificationProvider notificationProvider)
        {
            _notificationProvider = notificationProvider;
        }

        /// <summary>
        /// This handler will execute only when membership is requested to upgrade is brought
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public bool IsHandle(OrderItem orderItem)
        {
            return orderItem.Product == Product.UpgradeMembership;
        }

        /// <summary>
        /// handle upgrade membership 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public OrderItem ProcessOrderItem(Order order, OrderItem orderItem)
        {
            //Activate the requested membership 
            var upgradeMembership = $"Upgrade Membership to {orderItem.SKUId} for customerId {order.Customer.CustomerId}";
            //set the step
            orderItem.OrderProcessSteps = string.IsNullOrEmpty(orderItem.OrderProcessSteps) ? upgradeMembership : $"{orderItem.OrderProcessSteps},{upgradeMembership}";

            var activateUpgradedMembership = $"Activate Upgraded Membership - {orderItem.SKUId} for customerId {order.Customer.CustomerId}";
            //set the step
            orderItem.OrderProcessSteps = $"{orderItem.OrderProcessSteps},{activateUpgradedMembership}";
            //We can use json string for sending msg in notification. We need to send email to customer with upgrade details
            var notification = _notificationProvider.SendNotification(order, $"upgrade membership with skuId - {orderItem.SKUId} and details");
            //set the step
            orderItem.OrderProcessSteps = $"{orderItem.OrderProcessSteps},{notification}";

            return orderItem;
        }
    }
}
