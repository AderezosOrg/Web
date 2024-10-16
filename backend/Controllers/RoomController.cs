using backend.Services;
using backend.Services.AbstractClass;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;

    public RoomController(RoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("{roomId}")]
    public async Task<ActionResult<RoomFullInfoDTO>> GetRoomById(Guid roomId)
    {
        RoomFullInfoDTO room = await _roomService.GetElementById(roomId);
        return Ok(room);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<RoomFullInfoDTO>>> GetRooms()
    {
        List<RoomFullInfoDTO> rooms = await _roomService.GetAllElements();
        return Ok(rooms);
    }
    
    [HttpPost]
    public async Task<ActionResult<RoomPostDTO>> CreateRoom(RoomNewPostDTO roomDto)
    {
        RoomPostDTO room = await _roomService.CreateSingleElement(roomDto);
        return Ok(room);
    }
    
    [HttpGet("beds/{roomId}")]
    public async Task<ActionResult<List<BedDTO>>> GetRoomBedsById(Guid roomId)
    {
        List<BedDTO> beds = await _roomService.GetRoomBedsById(roomId);
        return Ok(beds);
    }
    
    [HttpGet("bathrooms/{roomId}")]
    public async Task<ActionResult<List<BathroomDTO>>> GetRoomBathRoomsById(Guid roomId)
    {
        List<BathroomDTO> bathrooms = await _roomService.GetRoomBathroomsById(roomId);
        return Ok(bathrooms);
    }
    
    [HttpGet("services/{roomId}")]
    public async Task<ActionResult<List<ServiceDTO>>> GetRoomServicesById(Guid roomId)
    {
        List<ServiceDTO> services = await _roomService.GetRoomServicesById(roomId);
        return Ok(services);
        
    }
    
}
