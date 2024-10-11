using DTOs.WithId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class BedController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<BedDTO>> GetBeds()
    {
        return Ok(new List<BedDTO>
        {
            new BedDTO
            {
                BedID = Guid.NewGuid(),
                BedQuantity = 2,
                Capacity = "2",
                Size = "king"
            }
        });
    }
    
    [HttpGet("{BedID}")]
    public ActionResult<BedDTO> GetBedById(Guid BedID)
    {
        return Ok(
            new BedDTO
            {
                BedID = Guid.NewGuid(),
                BedQuantity = 2,
                Capacity = "1",
                Size = "single"
            });
    }
    
    [HttpPost]
    public ActionResult<bool> CreateBed(BedDTO bedDTO)
    {
        return Ok(true);
    }
    
    [HttpPut("{BedID}")]
    public ActionResult<BedDTO> EditBed(Guid BedID, BedDTO bedDTO)
    {
        return Ok(new BedDTO
        {
            BedID = Guid.NewGuid(),
            BedQuantity = 1,
            Capacity = "1",
            Size = "queen"
        });
    }
    
    [HttpDelete("{BedID}")]
    public ActionResult<bool> DeleteBed(Guid BedID)
    {
        return Ok(true);
    }
}
