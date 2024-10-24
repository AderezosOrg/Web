using backend.Services.ServicesInterfaces;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly ISessionService _sessionService;

    public HotelController(IHotelService hotelService, ISessionService sessionService)
    {
        _hotelService = hotelService;
        _sessionService = sessionService;
    }

    [HttpGet("{hotelId}")]
    public async Task<ActionResult<HotelPostDTO>> GetHotelById(Guid hotelId)
    {
        HotelPostDTO hotel = await _hotelService.GetElementById(hotelId);
        return Ok(hotel);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<HotelDTO>>> GetHotels()
    {
        List<HotelDTO> hotels = await _hotelService.GetAllElements();
        return Ok(hotels);
    }
    
    [HttpPost]
    public async Task<ActionResult<HotelPostDTO>> CreateHotel([FromBody] HotelPostDTO hotelDto)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        HotelPostDTO hotel = await _hotelService.CreateSingleElement(hotelDto);
        return Ok(hotel);
    }
    
    [HttpPut("{hotelId}")]
    public async Task<ActionResult<HotelPostDTO>> UpdateHotel(Guid hotelId, HotelPostDTO hotelDto)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        HotelPostDTO result = await _hotelService.UpdateElementById(hotelId, hotelDto);
        return Ok(result);
    }
    
}
