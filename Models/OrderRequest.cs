namespace OrderPricingSystem.Models
{
    public class OrderRequest
    {
        public required string ProductId { get; set; }
        public int Quantity { get; set; }
        public required string Country { get; set; }
    }
}
