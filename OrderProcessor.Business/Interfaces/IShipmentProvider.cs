using OrderProcessor.DTO;

namespace OrderProcessor.Business.Interfaces
{
    public interface IShipmentProvider
    {
        string GeneratePackagingShip(Customer customer);
    }
}
