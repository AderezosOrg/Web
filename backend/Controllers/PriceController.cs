using backend.Services;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceController : ControllerBase
{
    private PriceService _priceService;

    public PriceController()
    {
        _priceService = new PriceService();
    }

    [HttpPost("/total")]
    public async Task<ActionResult<decimal>> GetReservationsTotalPrice(ReservationPostDTO[] reservationPostDtos)
    {
        var totalPrice = await _priceService.GetReservationPrice(reservationPostDtos);
        return Ok(totalPrice);
    }
    
    [HttpPost("/partial")]
    public async Task<ActionResult<decimal>> GetReservationsPartialPrice(ReservationPostDTO[] reservationPostDtos)
    {
        var partialPrice = await _priceService.GetReservationPartialPrice(reservationPostDtos);
        return Ok(partialPrice);
    }
    
    [HttpPost("/tax")]
    public async Task<ActionResult<decimal>> GetReservationsTaxPrice(ReservationPostDTO[] reservationPostDtos)
    {
        var taxPrice = await _priceService.GetReservationTaxPrice(reservationPostDtos);
        return Ok(taxPrice);
    }
}
