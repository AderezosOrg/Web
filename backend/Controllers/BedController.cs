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
    public async Task<ActionResult<List<BedInfoDTO>>> GetBeds()
    {
        
        var beds = await _bedService.GetAllElements();
        return Ok(beds);
        
    }
    
    [HttpGet("{bedId}")]
    public  async Task<ActionResult<BedPostDTO>> GetBedById(Guid bedId)
    {
        var bed = await _bedService.GetElementById(bedId);
        return Ok(bed);
    }
    
    [HttpPost]
    public async Task<ActionResult<BedPostDTO>> CreateBed(BedPostDTO bedDTO)
    {
        var bed = await _bedService.CreateSingleElement(bedDTO);
        return Ok(bed);
    }
    
    [HttpPut("{BedID}")]
    public async Task<ActionResult<BedPostDTO>> EditBed(Guid BedID, BedPostDTO bedDTO)
    {
        var bed = await _bedService.UpdateElementById(BedID, bedDTO);
        return Ok(bed);
    }
    
    [HttpDelete("{BedID}")]
    public async Task<ActionResult<bool>> DeleteBed(Guid BedID)
    {
        var confirm = await _bedService.DeleteElementById(BedID);
        return Ok(confirm);
    }
}
