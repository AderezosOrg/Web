using backend.Services.ServicesInterfaces;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomFiltersController : ControllerBase
{
    private IRoomFiltersService _roomFiltersService;

    public RoomFiltersController(IRoomFiltersService roomFiltersService)
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
    
    [HttpPost("priceRange")]
    public async Task<ActionResult<List<RoomDTO>>> GetRoomsByPriceRange(PriceRangeRequestDTO priceRangeRequestDto)
    {
        List<RoomDTO> rooms = await _roomFiltersService.GetRoomsByPriceRange(priceRangeRequestDto);
        return Ok(rooms);
    }

}
