using OrderProcessor.Business.Interfaces;
using OrderProcessor.DTO;

namespace OrderProcessor.Business.Concrete
{
    public class EmailProvider : INotificationProvider
    {
        public string SendNotification(Order order, string msg)
        {
            return $"Send email to {order.Customer.Email} for {msg}";
        }
    }
}
