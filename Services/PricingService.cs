using System.Text.Json;
using OrderPricingSystem.Models;

namespace OrderPricingSystem.Services;

public class PricingService
{
    private readonly ILogger<PricingService> _logger;
    private readonly string _productFilePath = "Data/products.json";

    public PricingService(ILogger<PricingService> logger)
    {
        _logger = logger;
    }

    public PricingResponse CalculatePrice(OrderRequest request)
    {
        ValidateRequest(request);

        var product = GetProduct(request.ProductId);

        decimal subtotal = request.Quantity * product.Price;

        decimal discountPct = CalculateDiscount(request.Quantity, subtotal);
        decimal discountAmount = subtotal * discountPct;
        decimal subtotalAfterDiscount = subtotal - discountAmount;

        decimal taxRate = GetTaxRate(request.Country);
        decimal taxAmount = subtotalAfterDiscount * taxRate;

        decimal finalPrice = subtotalAfterDiscount + taxAmount;

        return BuildResponse(product, request, subtotal,
            discountPct, discountAmount,
            subtotalAfterDiscount,
            taxRate, taxAmount, finalPrice);
    }

    private void ValidateRequest(OrderRequest request)
    {
        if (request == null)
            throw new ArgumentException("Request cannot be null.");

        if (request.Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0.");

        if (string.IsNullOrWhiteSpace(request.ProductId))
            throw new ArgumentException("ProductId is required.");
    }

    private decimal CalculateDiscount(int quantity, decimal subtotal)
    {
        if (subtotal < 500)
            return 0m;

        if (quantity >= 100)
            return 0.15m;
        else if (quantity >= 50)
            return 0.10m;
        else if (quantity >= 10)
            return 0.05m;

        return 0m;
    }

    private decimal GetTaxRate(string country)
    {
        return country switch
        {
            "MK" => 0.18m,
            "DE" => 0.20m,
            "FR" => 0.20m,
            "USA" => 0.10m,
            _ => throw new ArgumentException("Unsupported country.")
        };
    }

    private Product GetProduct(string productId)
    {
        if (!File.Exists(_productFilePath))
            throw new FileNotFoundException("products.json not found.");

        var json = File.ReadAllText(_productFilePath);
       
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var data = JsonSerializer.Deserialize<ProductData>(json, options);
       
        if (data?.Products == null)
        {
            throw new Exception("Failed to deserialize products or JSON is empty.");
        }

        var product = data.Products.FirstOrDefault(p => p.Id == productId);

        if (product == null)
            throw new ArgumentException($"Product with ID '{productId}' not found.");

        return product;
    }

    private PricingResponse BuildResponse(
        Product product,
        OrderRequest request,
        decimal subtotal,
        decimal discountPct,
        decimal discountAmount,
        decimal subtotalAfterDiscount,
        decimal taxRate,
        decimal taxAmount,
        decimal finalPrice)
    {
        return new PricingResponse
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Quantity = request.Quantity,
            UnitPrice = product.Price,
            Country = request.Country,
            Subtotal = subtotal,
            Discount = new DiscountInfo
            {
                Percentage = discountPct,
                Amount = discountAmount
            },
            SubtotalAfterDiscount = subtotalAfterDiscount,
            Tax = new TaxInfo
            {
                Country = request.Country,
                Rate = taxRate,
                Amount = taxAmount
            },
            FinalPrice = finalPrice
        };
    }

    private class ProductData
    {
        public required List<Product> Products { get; set; }
    }
}