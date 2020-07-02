using OrderProcessor.Business.Interfaces;
using OrderProcessor.DTO;

namespace OrderProcessor.Business.Concrete
{
    /// <summary>
    /// Shipment provider
    /// </summary>
    public class ShipmentProvider : IShipmentProvider
    {
        /// <summary>
        /// this the method can be used to generate the shipment slip
        /// </summary>
        /// <returns></returns>
        public string GeneratePackagingShip(Customer customer)
        {
            // We can use the customer object to generate the shipment slip
            return $"Generate slip for {customer.CustomerName}";
        }
    }
}
