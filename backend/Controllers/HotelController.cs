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

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
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
        HotelPostDTO hotel = await _hotelService.CreateSingleElement(hotelDto);
        return Ok(true);
    }
    
    [HttpPut("{hotelId}")]
    public async Task<ActionResult<HotelPostDTO>> UpdateHotel(Guid hotelId, HotelPostDTO hotelDto)
    {
        HotelPostDTO result = await _hotelService.UpdateElementById(hotelId, hotelDto);
        return Ok(result);
    }
    
}
