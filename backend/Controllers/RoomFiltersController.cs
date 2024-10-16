using backend.Services.AbstractClass;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomFiltersController : ControllerBase
{
    private RoomFiltersService _roomFiltersService;

    public RoomFiltersController(RoomFiltersService roomFiltersService)
    {
        _roomFiltersService = roomFiltersService;
    }

    [HttpPost("available")]
    public async Task<ActionResult<List<RoomFullInfoDTO>>> GetAvailableRooms(AvailabilityRequestDTO availabilityRequestDto)
    {
        List<RoomFullInfoDTO> roomDtos = await _roomFiltersService.GetAvailableRooms(availabilityRequestDto);
        return Ok(roomDtos);
    }
    
    [HttpGet("floor/{floorNumber}")]
    public async Task<ActionResult<List<RoomDTO>>> GetRoomsByFloor(int floorNumber)
    {
        List<RoomDTO> rooms = await _roomFiltersService.GetRoomsByFloor(floorNumber);
        return Ok(rooms);
    }
    
    [HttpGet("prices/{minPrice}/{maxPrice}")]
    public async Task<ActionResult<List<RoomDTO>>> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice)
    {
        List<RoomDTO> rooms = await _roomFiltersService.GetRoomsByPriceRange(minPrice, maxPrice);
        return Ok(rooms);
    }

}