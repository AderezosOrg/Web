using backend.Services.ServicesInterfaces;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomFiltersController : ControllerBase
{
    private readonly IRoomFiltersService _roomFiltersService;
    private readonly ISessionService _sessionService;

    public RoomFiltersController(IRoomFiltersService roomFiltersService, ISessionService sessionService)
    {
        _roomFiltersService = roomFiltersService;
        _sessionService = sessionService;
    }

    [HttpPost("available")]
    public async Task<ActionResult<List<RoomFullInfoDTO>>> GetAvailableRooms(AvailabilityRequestDTO availabilityRequestDto)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
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
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        List<RoomDTO> rooms = await _roomFiltersService.GetRoomsByPriceRange(priceRangeRequestDto);
        return Ok(rooms);
    }

}
