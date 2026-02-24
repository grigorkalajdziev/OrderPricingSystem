using Microsoft.AspNetCore.Mvc;
using OrderPricingSystem.Models;
using OrderPricingSystem.Services;

namespace OrderPricingSystem.Controllers;

[ApiController]
[Route("api/pricing")]
public class PricingController : ControllerBase
{
    private readonly PricingService _pricingService;

    public PricingController(PricingService pricingService)
    {
        _pricingService = pricingService;
    }

    [HttpGet("calculate")]
    public IActionResult Calculate(
        [FromQuery] string productId,
        [FromQuery] int quantity,
        [FromQuery] string country)
    {
        try
        {
            var request = new OrderRequest
            {
                ProductId = productId,
                Quantity = quantity,
                Country = country
            };

            var result = _pricingService.CalculatePrice(request);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}