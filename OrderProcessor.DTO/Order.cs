using OrderProcessor.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessor.DTO
{
    public class Order
    {
        public int OrderId { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int AgentId { get; set; }
    }
}
