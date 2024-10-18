using backend.Services.ServicesInterfaces;
using DTOs.WithId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomInfoController : ControllerBase
{
    private readonly IRoomInfoService _roomInfoService;

    public RoomInfoController(IRoomInfoService roomInfoService)
    {
        _roomInfoService = roomInfoService;
    }
    [HttpGet("beds/{roomId}")]
    public async Task<ActionResult<List<BedDTO>>> GetRoomBedsById(Guid roomId)
    {
        List<BedDTO> beds = await _roomInfoService.GetRoomBedsById(roomId);
        return Ok(beds);
    }
    
    [HttpGet("bathrooms/{roomId}")]
    public async Task<ActionResult<List<BathroomDTO>>> GetRoomBathRoomsById(Guid roomId)
    {
        List<BathroomDTO> bathrooms = await _roomInfoService.GetRoomBathroomsById(roomId);
        return Ok(bathrooms);
    }
    
    [HttpGet("services/{roomId}")]
    public async Task<ActionResult<List<ServiceDTO>>> GetRoomServicesById(Guid roomId)
    {
        List<ServiceDTO> services = await _roomInfoService.GetRoomServicesById(roomId);
        return Ok(services);
        
    }
}
