namespace OrderPricingSystem.Models
{
    public class Product
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
