using backend.Services;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceController : ControllerBase
{
    private PriceService _priceService;

    public PriceController(PriceService priceService)
    {
        _priceService = priceService;
    }

    [HttpPost("/total")]
    public async Task<ActionResult<decimal>> GetReservationsTotalPrice(PriceRequestsDTO reservations)
    {
        var totalPrice = await _priceService.GetReservationPrice(reservations);
        return Ok(totalPrice);
    }
    
    [HttpPost("/partial")]
    public async Task<ActionResult<decimal>> GetReservationsPartialPrice(PriceRequestsDTO reservations)
    {
        var partialPrice = await _priceService.GetReservationPartialPrice(reservations);
        return Ok(partialPrice);
    }
    
    [HttpPost("/tax")]
    public async Task<ActionResult<decimal>> GetReservationsTaxPrice(PriceRequestsDTO reservations)
    {
        var taxPrice = await _priceService.GetReservationTaxPrice(reservations);
        return Ok(taxPrice);
    }
}
