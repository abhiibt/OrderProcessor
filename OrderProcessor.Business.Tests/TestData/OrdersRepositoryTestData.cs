using OrderProcessor.DTO;
using OrderProcessor.DTO.Common;
using System.Collections.Generic;

namespace OrderProcessor.Business.Tests.TestData
{
    public static class OrdersRepositoryTestData
    {
      
        public static IEnumerable<Order> OneItemOrderPassProductType(Product product)
        {
            var orders = new List<Order>();
            var order1 = new Order()
            {
                OrderId = 1,
                AgentId = 10001,
                OrderStatus = DTO.Common.OrderStatus.New,
                Customer = new Customer()
                {
                    CustomerId = 1,
                    CustomerName = "Abhijit",
                    Phone = "+91-9000090000",
                    Email = "abc@email.com",
                    BillTo = new Address()
                    {
                        AddressId = 1,
                        AddressLine1 = "Door 1, xyz Appartment",
                        AddressLine2 = "abc Street",
                        City = "Bangalore",
                        State = "Karnataka",
                        Country = "India",
                        ZipCode = "6000012"
                    },
                    ShipTo = new Address()
                    {
                        AddressId = 1,
                        AddressLine1 = "Door 1, xyz Appartment",
                        AddressLine2 = "abc Street",
                        City = "Bangalore",
                        State = "Karnataka",
                        Country = "India",
                        ZipCode = "6000012"
                    }
                },
                OrderItems = new List<OrderItem>() {new OrderItem()
                {
                    OrderItemId = 1,
                    Product = product,
                    Price = 1000m,
                    SKUId = 100123
                } }

            };
            orders.Add(order1);
            return orders;
        }
    }
}
