using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessor.DTO
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Address BillTo { get; set; }
        public Address ShipTo { get; set; }
    }
}
