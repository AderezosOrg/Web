using DTOs.WithId;
using backend.Services;
using DTOs.WithoutId;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class BedController : ControllerBase
{
    private BedService _bedService;
    public BedController(BedService bedService)
    {
        _bedService = bedService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BedDTO>>> GetBeds()
    {
        
        var beds = await _bedService.GetBeds();
        return Ok();
        
    }
    
    [HttpGet("{bedId}")]
    public  async Task<ActionResult<BedDTO>> GetBedById(Guid bedId)
    {
        return Ok(await new BedService().GetBedById(bedId));
    }
    
    [HttpPost]
    public async Task<ActionResult<BedPostDTO>> CreateBed(BedPostDTO bedDTO)
    {
        return Ok(await new BedService().CreateBed(bedDTO));
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
