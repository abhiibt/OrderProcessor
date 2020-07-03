using OrderProcessor.Business.Interfaces;
using OrderProcessor.DTO;
using OrderProcessor.DTO.Common;

namespace OrderProcessor.Business.Concrete
{
    public class CommissionProviderHandler : IRuleHandler
    {
        /// <summary>
        /// This handler will execute only for Physical/Book type of product is brought
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public bool IsHandle(OrderItem orderItem)
        {
            return orderItem.Product == Product.PhysicalProduct || orderItem.Product == Product.Book;
        }

        /// <summary>
        /// Commission handler for physical product/Book
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public OrderItem ProcessOrderItem(Order order, OrderItem orderItem)
        {
            // might be we can use the payment provider for raising the payment for the agent. Using dependency injection.
            //Generate shipping slip for physical product
            var raiseAgentPayment = $"Raise payment for the Agent {order.AgentId}";
            //set the step
            orderItem.OrderProcessSteps = string.IsNullOrEmpty(orderItem.OrderProcessSteps) ? raiseAgentPayment : $"{orderItem.OrderProcessSteps},{raiseAgentPayment}";
            return orderItem;
        }
    }
}
