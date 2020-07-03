using OrderProcessor.DTO;

namespace OrderProcessor.Business.Interfaces
{
    public interface INotificationProvider
    {
        string SendNotification(Order order, string msg);
    }
}
