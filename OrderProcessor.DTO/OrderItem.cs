using OrderProcessor.DTO.Common;

namespace OrderProcessor.DTO
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int SKUId { get; set; }
        public string OrderProcessSteps { get; set; }
    }
}
