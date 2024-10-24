using backend.Services.ServicesInterfaces;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly ISessionService _sessionService;

    public RoomController(IRoomService roomService, ISessionService sessionService)
    {
        _roomService = roomService;
        _sessionService = sessionService;
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
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        RoomPostDTO room = await _roomService.CreateSingleElement(roomDto);
        return Ok(room);
    }
    
}
