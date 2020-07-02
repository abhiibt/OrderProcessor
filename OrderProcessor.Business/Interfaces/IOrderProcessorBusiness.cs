using OrderProcessor.DTO;

namespace OrderProcessor.Business.Interfaces
{
    public interface IOrderProcessorBusiness
    {
        Order SubmitOrder(Order order);
    }
}
