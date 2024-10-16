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
    public async Task<ActionResult<List<BathroomInfoDTO>>> GetBathRooms()
    {
        List<BathroomInfoDTO> bathrooms = await _bathRoomServices.GetAllElements(); 
        return Ok(bathrooms);
    }
    
    [HttpGet("{bathroomId}")]
    public async Task<ActionResult<BathroomPostDTO>> GetBathRoomById(Guid bathroomId)
    {
        BathroomPostDTO bathroom = await _bathRoomServices.GetElementById(bathroomId);
        return Ok(bathroom);
    }
    
    [HttpPost]
    public async Task<ActionResult<BathroomPostDTO>> CreateBathRoom(BathroomPostDTO bathroomDto)
    {
        BathroomPostDTO newBathRoom = await _bathRoomServices.CreateSingleElement(bathroomDto);
        return Ok(newBathRoom);
    }
    
    [HttpPut("{bathroomId}")]
    public async Task<ActionResult<BathroomPostDTO>> EditBathRoom(Guid bathroomId, BathroomPostDTO bathroomDto)
    {
        BathroomPostDTO editBathRoom = await _bathRoomServices.UpdateElementById(bathroomId, bathroomDto);
        return Ok(editBathRoom);
    }
    
    [HttpDelete("{bathroomId}")]
    public async Task<ActionResult<bool>> DeleteBathRoom(Guid bathroomId)
    {
        bool confirm = await _bathRoomServices.DeleteElementById(bathroomId);
        return Ok(confirm);
    }
}
