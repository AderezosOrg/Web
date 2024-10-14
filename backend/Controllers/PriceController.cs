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

    [HttpPost]
    public async Task<ActionResult<decimal>> GetReservationsTotalPrice(ReservationPostDTO[] reservationPostDtos)
    {
        var totalPrice = await _priceService.GetReservationPrice(reservationPostDtos);
        return Ok(totalPrice);
    }
}