using backend.Services.ServicesInterfaces;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceController : ControllerBase
{
    private readonly IPriceService _priceService;
    private readonly ISessionService _sessionService;

    public PriceController(IPriceService priceService, ISessionService sessionService)
    {
        _priceService = priceService;
        _sessionService = sessionService;
    }

    [HttpPost("total")]
    public async Task<ActionResult<decimal>> GetReservationsTotalPrice(PriceRequestsDTO reservations)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        var totalPrice = await _priceService.GetReservationPrice(reservations);
        return Ok(totalPrice);
    }
    
    [HttpPost("partial")]
    public async Task<ActionResult<decimal>> GetReservationsPartialPrice(PriceRequestsDTO reservations)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        var partialPrice = await _priceService.GetReservationPartialPrice(reservations);
        return Ok(partialPrice);
    }
    
    [HttpPost("tax")]
    public async Task<ActionResult<decimal>> GetReservationsTaxPrice(PriceRequestsDTO reservations)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        var taxPrice = await _priceService.GetReservationTaxPrice(reservations);
        return Ok(taxPrice);
    }
}
