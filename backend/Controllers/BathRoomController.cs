using backend.Services.ServicesInterfaces;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BathRoomController : ControllerBase
{
    private readonly IBathRoomService _bathRoomService;
    private readonly ISessionService _sessionService;
    public BathRoomController(IBathRoomService bathRoomService, ISessionService sessionService)
    {
        _bathRoomService = bathRoomService;
        _sessionService = sessionService;
    }
    [HttpGet]
    public async Task<ActionResult<List<BathroomInfoDTO>>> GetBathRooms()
    {
        List<BathroomInfoDTO> bathrooms = await _bathRoomService.GetAllElements(); 
        return Ok(bathrooms);
    }
    
    [HttpGet("{bathroomId}")]
    public async Task<ActionResult<BathroomPostDTO>> GetBathRoomById(Guid bathroomId)
    {
        BathroomPostDTO bathroom = await _bathRoomService.GetElementById(bathroomId);
        return Ok(bathroom);
    }
    
    [HttpPost]
    public async Task<ActionResult<BathroomPostDTO>> CreateBathRoom(BathroomPostDTO bathroomDto)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        BathroomPostDTO newBathRoom = await _bathRoomService.CreateSingleElement(bathroomDto);
        return Ok(newBathRoom);
    }
    
    [HttpPut("{bathroomId}")]
    public async Task<ActionResult<BathroomPostDTO>> EditBathRoom(Guid bathroomId, BathroomPostDTO bathroomDto)
    {
        if (!await _sessionService.IsTokenValid(Guid.Parse(Request.Headers["SessionId"])))
        {
            return Redirect("http://localhost:5173/");
        }
        BathroomPostDTO editBathRoom = await _bathRoomService.UpdateElementById(bathroomId, bathroomDto);
        return Ok(editBathRoom);
    }
    
    [HttpDelete("{bathroomId}")]
    public async Task<ActionResult<bool>> DeleteBathRoom(Guid bathroomId)
    {
        bool confirm = await _bathRoomService.DeleteElementById(bathroomId);
        return Ok(confirm);
    }
}
