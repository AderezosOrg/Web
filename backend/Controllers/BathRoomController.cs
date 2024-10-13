using backend.Services;
using DTOs.WithId;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class BathRoomController : ControllerBase
{
    private readonly BathRoomServices _bathRoomServices;
    public BathRoomController(BathRoomServices bathRoomService)
    {
        _bathRoomServices = bathRoomService;
    }
    [HttpGet]
    public async Task<ActionResult<List<BathroomDTO>>> GetBathRooms()
    {
        List<BathroomDTO> bathrooms = await _bathRoomServices.GetBathRooms(); 
        return Ok(bathrooms);
    }
    
    [HttpGet("{bathroomId}")]
    public async Task<ActionResult<BathroomPostDTO>> GetBathRoomById(Guid bathroomId)
    {
        BathroomPostDTO bathroom = await _bathRoomServices.GetBathRoomById(bathroomId);
        return Ok(bathroom);
    }
    
    [HttpPost]
    public async Task<ActionResult<BathroomPostDTO>> CreateBathRoom(BathroomPostDTO bathroomDto)
    {
        BathroomPostDTO newBathRoom = await _bathRoomServices.CreateBathRoom(bathroomDto);
        return Ok(newBathRoom);
    }
    
    [HttpPut("{bathroomId}")]
    public async Task<ActionResult<BathroomPostDTO>> EditBathRoom(Guid bathroomId, BathroomPostDTO bathroomDto)
    {
        BathroomPostDTO editBathRoom = await _bathRoomServices.EditBathRoom(bathroomId, bathroomDto);
        return Ok(editBathRoom);
    }
    
    [HttpDelete("{bathroomId}")]
    public async Task<ActionResult<bool>> DeleteBathRoom(Guid bathroomId)
    {
        bool confirm = await _bathRoomServices.DeleteBathRoom(bathroomId);
        return Ok(confirm);
    }
}
