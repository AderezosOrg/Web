using backend.Services;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly HotelService _hotelService;

    public HotelController(HotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet("{hotelId}")]
    public async Task<ActionResult<HotelPostDTO>> GetHotelById(Guid hotelId)
    {
        HotelPostDTO hotel = await _hotelService.GetHotelById(hotelId);
        return Ok(hotel);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<HotelDTO>>> GetHotels()
    {
        List<HotelDTO> hotels = await _hotelService.GetHotels();
        return Ok(hotels);
    }
    
    [HttpPost]
    public async Task<ActionResult<HotelPostDTO>> CreateHotel([FromBody] HotelPostDTO hotelDto)
    {
        HotelPostDTO hotel = await _hotelService.CreateHotel(hotelDto);
        return Ok(true);
    }
    
    [HttpPut("{hotelId}")]
    public async Task<ActionResult<HotelPostDTO>> UpdateHotel(Guid hotelId, HotelPostDTO hotelDto)
    {
        HotelPostDTO result = await _hotelService.UpdateHotel(hotelId, hotelDto);
        return Ok(result);
    }
    
}
