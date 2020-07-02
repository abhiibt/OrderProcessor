using OrderProcessor.DTO;

namespace OrderProcessor.Business.Interfaces
{
    public interface IRuleHandler
    {
        bool IsHandle(OrderItem orderItem);
        OrderItem ProcessOrderItem(Order order, OrderItem orderItem);
    }
}
