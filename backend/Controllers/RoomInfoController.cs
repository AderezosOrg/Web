using backend.Services;
using DTOs.WithId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomInfoController : ControllerBase
{
    private readonly RoomInfoServices _roomInfoServices;

    public RoomInfoController(RoomInfoServices roomInfoServices)
    {
        _roomInfoServices = roomInfoServices;
    }
    [HttpGet("beds/{roomId}")]
    public async Task<ActionResult<List<BedDTO>>> GetRoomBedsById(Guid roomId)
    {
        List<BedDTO> beds = await _roomInfoServices.GetRoomBedsById(roomId);
        return Ok(beds);
    }
    
    [HttpGet("bathrooms/{roomId}")]
    public async Task<ActionResult<List<BathroomDTO>>> GetRoomBathRoomsById(Guid roomId)
    {
        List<BathroomDTO> bathrooms = await _roomInfoServices.GetRoomBathroomsById(roomId);
        return Ok(bathrooms);
    }
    
    [HttpGet("services/{roomId}")]
    public async Task<ActionResult<List<ServiceDTO>>> GetRoomServicesById(Guid roomId)
    {
        List<ServiceDTO> services = await _roomInfoServices.GetRoomServicesById(roomId);
        return Ok(services);
        
    }
}
