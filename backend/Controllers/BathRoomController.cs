using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class BathRoomController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<BathroomDTO>> GetBathRooms()
    {
        return Ok(new List<BathroomDTO>
        {
            new BathroomDTO()
            {
                BathRoomID = Guid.NewGuid(),
                BathroomQuantity = 2,
                Shower = false,
                DressingTable = true,
                Toilet = false
            }
        });
    }
    
    [HttpGet("{BathroomID}")]
    public ActionResult<BathroomDTO> GetBathRoomById(Guid BathroomID)
    {
        return Ok(
            new BathroomDTO()
            {
                BathRoomID = Guid.NewGuid(),
                BathroomQuantity = 2,
                Shower = true,
                DressingTable = true,
                Toilet = false
            });
    }
    
    [HttpPost]
    public ActionResult<bool> CreateBathRoom(BathroomDTO bathroomDto)
    {
        return Ok(true);
    }
    
    [HttpPut("{BathroomID}")]
    public ActionResult<BathroomDTO> EditBathRoom(Guid BathroomID, BathroomDTO bathroomDto)
    {
        return Ok(new BathroomDTO()
        {
            BathRoomID = Guid.NewGuid(),
            BathroomQuantity = 2,
            Shower = false,
            DressingTable = true,
            Toilet = true
        });
    }
    
    [HttpDelete("{BathroomID}")]
    public ActionResult<bool> DeleteBathRoom(Guid BathroomID)
    {
        return Ok(true);
    }
}
