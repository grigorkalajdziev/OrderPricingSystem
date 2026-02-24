namespace OrderPricingSystem.Models;

public class PricingResponse
{
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public required string Country { get; set; }
    public decimal Subtotal { get; set; }
    public required DiscountInfo Discount { get; set; }
    public decimal SubtotalAfterDiscount { get; set; }
    public required TaxInfo Tax { get; set; }
    public decimal FinalPrice { get; set; }
}

public class DiscountInfo
{
    public decimal Percentage { get; set; }
    public decimal Amount { get; set; }
}

public class TaxInfo
{
    public required string Country { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
}